@echo off
REM Script to generate HTML coverage reports from existing coverage data
REM Usage: generate-html-report.bat [format]
REM Format options: cobertura, opencover, lcov, json (default: cobertura)

SETLOCAL ENABLEDELAYEDEXPANSION

SET "Format=cobertura"
SET "CoverageDir=%CD%\coverage"
SET "HtmlReportDir=%CD%\coverage\html"

REM Parse arguments
IF NOT "%~1"=="" SET "Format=%~1"
SET "ReportFile=%CoverageDir%\coverage.%Format%.xml"

REM Handle formats that don't include .xml
IF /I "%Format%"=="json" SET "ReportFile=%CoverageDir%\coverage.%Format%"
IF /I "%Format%"=="cobertura" SET "ReportFile=%CoverageDir%\coverage.cobertura.xml"
IF /I "%Format%"=="opencover" SET "ReportFile=%CoverageDir%\coverage.opencover.xml"
IF /I "%Format%"=="lcov" SET "ReportFile=%CoverageDir%\coverage.lcov"

ECHO.
ECHO ======================================
ECHO Generating HTML coverage report...
ECHO ======================================
ECHO Source: %ReportFile%
ECHO Output: %HtmlReportDir%
ECHO.

REM Check if coverage report exists
IF NOT EXIST "%ReportFile%" (
  ECHO ERROR: Coverage report not found: %ReportFile%
  ECHO.
  ECHO Please run 'run-coverage.bat' first to generate coverage data.
  EXIT /B 1
)

REM Check if reportgenerator is installed
WHERE reportgenerator >nul 2>nul
IF %ERRORLEVEL% NEQ 0 (
  ECHO WARNING: ReportGenerator not found. Installing...
  CALL dotnet tool install -g dotnet-reportgenerator-globaltool
  IF %ERRORLEVEL% NEQ 0 (
    ECHO ERROR: Failed to install ReportGenerator
    EXIT /B 1
  )
)

REM Generate HTML report
reportgenerator ^
  "-reports:%ReportFile%" ^
  "-targetdir:%HtmlReportDir%" ^
  "-reporttypes:Html;HtmlSummary;MarkdownSummary" ^
  "-verbosity:Warning"

IF %ERRORLEVEL% EQU 0 (
  ECHO.
  ECHO ✓ HTML report generation successful!
  ECHO.
  ECHO Report generated at: %HtmlReportDir%
  ECHO.
  ECHO View the report in your browser:
  ECHO   START "%HtmlReportDir%\index.html"
  ECHO.
  ECHO Markdown summary available at:
  ECHO   %HtmlReportDir%\summary.md
  ECHO.
) ELSE (
  ECHO ERROR: Failed to generate HTML report!
  EXIT /B 1
)

ENDLOCAL
