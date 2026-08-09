#!/usr/bin/env python3
"""Phase 0 read-only validator: checks Content Catalog ID tables for
duplicate IDs and IDs that don't match the documented namespace
convention (res./good./bld./ship./veh./resrch.<domain>.<...>).

Read-only: reports findings, never modifies source documents.
Usage: python3 Tools/Validation/check_content_ids.py [path-to-content-catalog.md]
"""
import re
import sys
from collections import defaultdict
from pathlib import Path

DEFAULT_TARGET = "Docs/01_DESIGN/CONTENT_CATALOG_v1.0.md"

ID_PREFIXES = ("res.", "good.", "bld.", "ship.", "veh.", "resrch.")
# dotted, lowercase, alnum + dot/underscore segments
ID_PATTERN = re.compile(r"^[a-z0-9]+(\.[a-z0-9_]+)+$")

TABLE_ROW = re.compile(r"^\|\s*([^\|]+?)\s*\|")


def extract_candidate_ids(lines):
    ids = []
    for lineno, line in enumerate(lines, start=1):
        if not line.startswith("|"):
            continue
        if set(line.strip()) <= {"|", "-", " "}:
            continue  # markdown table separator row
        m = TABLE_ROW.match(line)
        if not m:
            continue
        first_cell = m.group(1).strip()
        if any(first_cell.startswith(p) for p in ID_PREFIXES):
            ids.append((lineno, first_cell))
    return ids


def main():
    target = Path(sys.argv[1]) if len(sys.argv) > 1 else Path(DEFAULT_TARGET)
    if not target.exists():
        print(f"FAIL: target file not found: {target}")
        return 1

    lines = target.read_text(encoding="utf-8").splitlines()
    candidates = extract_candidate_ids(lines)

    seen = defaultdict(list)
    malformed = []
    for lineno, cid in candidates:
        seen[cid].append(lineno)
        if not ID_PATTERN.match(cid):
            malformed.append((lineno, cid))

    duplicates = {cid: lns for cid, lns in seen.items() if len(lns) > 1}

    print(f"Target: {target}")
    print(f"Candidate IDs scanned: {len(candidates)}")
    print(f"Unique IDs: {len(seen)}")
    print()

    if duplicates:
        print(f"DUPLICATE IDs found: {len(duplicates)}")
        for cid, lns in sorted(duplicates.items()):
            print(f"  - {cid}  (lines: {', '.join(map(str, lns))})")
    else:
        print("No duplicate IDs found.")
    print()

    if malformed:
        print(f"MALFORMED IDs (don't match dotted-lowercase convention): {len(malformed)}")
        for lineno, cid in malformed:
            print(f"  - line {lineno}: {cid}")
    else:
        print("No malformed IDs found (all match dotted-lowercase convention).")

    print()
    status = "PASS" if not duplicates and not malformed else "FAIL"
    print(f"RESULT: {status}")
    return 0 if status == "PASS" else 1


if __name__ == "__main__":
    raise SystemExit(main())
