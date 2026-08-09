#!/usr/bin/env python3
"""Phase 0 read-only validator: scans Markdown files under Docs/ for
relative link/file references and reports any that point to a
non-existent file.

Read-only: reports findings, never modifies source documents.
Usage: python3 Tools/Validation/check_markdown_links.py [root-dir]
"""
import re
import sys
from pathlib import Path

DEFAULT_ROOT = "Docs"

# Matches Markdown links/images: [text](target) — ignores bare URLs (http/https/mailto)
# and anchors-only links (#section).
LINK_PATTERN = re.compile(r"!?\[[^\]]*\]\(([^)]+)\)")


def is_external_or_anchor(target: str) -> bool:
    target = target.strip()
    if not target or target.startswith("#"):
        return True
    if re.match(r"^[a-zA-Z][a-zA-Z0-9+.\-]*://", target):
        return True
    if target.startswith("mailto:"):
        return True
    return False


def main():
    root = Path(sys.argv[1]) if len(sys.argv) > 1 else Path(DEFAULT_ROOT)
    if not root.exists():
        print(f"FAIL: root not found: {root}")
        return 1

    md_files = sorted(root.rglob("*.md"))
    broken = []
    total_links = 0

    for md_file in md_files:
        text = md_file.read_text(encoding="utf-8", errors="ignore")
        for lineno, line in enumerate(text.splitlines(), start=1):
            for match in LINK_PATTERN.finditer(line):
                target = match.group(1).strip()
                if is_external_or_anchor(target):
                    continue
                total_links += 1
                # strip trailing anchor fragment
                target_path = target.split("#", 1)[0]
                if not target_path:
                    continue
                resolved = (md_file.parent / target_path).resolve()
                if not resolved.exists():
                    broken.append((str(md_file), lineno, target))

    print(f"Root: {root}")
    print(f"Markdown files scanned: {len(md_files)}")
    print(f"Relative links checked: {total_links}")
    print()

    if broken:
        print(f"BROKEN relative links found: {len(broken)}")
        for file, lineno, target in broken:
            print(f"  - {file}:{lineno} -> {target}")
    else:
        print("No broken relative links found.")

    print()
    status = "PASS" if not broken else "FAIL"
    print(f"RESULT: {status}")
    return 0 if status == "PASS" else 1


if __name__ == "__main__":
    raise SystemExit(main())
