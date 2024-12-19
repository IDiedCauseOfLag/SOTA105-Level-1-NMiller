#!/bin/sh
echo -ne '\033c\033]0;Sota 105-level-1\a'
base_path="$(dirname "$(realpath "$0")")"
"$base_path/Build2MovementTest.x86_64" "$@"
