#!/usr/bin/env bash
set -euo pipefail
cd "$(dirname "$0")"
if ! java -version 2>&1 | grep -qE 'version "25'; then
  echo 'JDK 25 is required. Set JAVA_HOME to your Java 25 install.' >&2
  exit 1
fi
VERSION=9.8.2
GRADLE=".gradle-dist/gradle-$VERSION/bin/gradle"
if [[ ! -x "$GRADLE" ]]; then
  mkdir -p .gradle-dist
  curl -fL "https://services.gradle.org/distributions/gradle-$VERSION-bin.zip" -o ".gradle-dist/gradle-$VERSION-bin.zip"
  (cd .gradle-dist && unzip -q "gradle-$VERSION-bin.zip" && rm "gradle-$VERSION-bin.zip")
fi
"$GRADLE" --no-daemon build
