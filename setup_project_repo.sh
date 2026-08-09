#!/usr/bin/env bash
set -euo pipefail

ROOT="${1:-.}"
cd "$ROOT"

if [[ ! -d Assets || ! -d ProjectSettings || ! -d Packages ]]; then
  echo "FEHLER: Das sieht nicht wie der Root eines bereits erstellten Unity-Projekts aus."
  echo "Erstelle zuerst in Unity Hub ein Unity-6.5 Universal-3D/URP-Projekt und entpacke danach das PASS10-Paket hinein."
  exit 1
fi

git init
git lfs install

echo
echo "Git/LFS initialisiert."
echo "Prüfe jetzt:"
echo "  git status"
echo "Danach erster Commit, sobald Unity einmal sauber geöffnet/geschlossen wurde."
