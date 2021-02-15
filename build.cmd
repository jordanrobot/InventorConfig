:; set -eo pipefail
:; SCRIPT_DIR=$(cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd)
:; ${SCRIPT_DIR}/build.sh "$@"
:; exit $?

@ECHO OFF
gitversion /UpdateAssemblyInfo
powershell -ExecutionPolicy ByPass -NoProfile "%~dp0build.ps1" %*
