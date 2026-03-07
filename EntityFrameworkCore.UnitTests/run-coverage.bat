@echo off
REM Script to run tests with code coverage and generate HTML reports
REM Usage: run-coverage.bat [format] [generateHtml]
REM Format options: cobertura, opencover, lcov, json (default: cobertura)
REM generateHtml: true/false (default: true)

SETLOCAL ENABLEDELAYEDEXPANSION

SET "Format=cobertura"
SET "MinimumCoverage=0"
SET "GenerateHtml=true"
SET "ProjectPath=EntityFrameworkCore.UnitTests.csproj"
SET "CoverageDir=%CD%\coverage"
SET "HtmlReportDir=%CD%\coverage\html"

REM Parse arguments
IF NOT "%~1"=="" SET "Format=%~1"
IF NOT "%~2"=="" SET "GenerateHtml=%~2"

ECHO.
ECHO ======================================
ECHO Running tests with code coverage...
ECHO ======================================
ECHO Format: %Format%
ECHO HTML Report: %GenerateHtml%
ECHO Coverage Directory: %CoverageDir%
ECHO.

REM Run tests with coverage
dotnet test "%ProjectPath%" ^
  /p:CollectCoverage=true ^
  /p:CoverletOutput="%CoverageDir%\" ^
  /p:CoverletOutputFormat=%Format% ^
  /p:Threshold=%MinimumCoverage%

IF %ERRORLEVEL% NEQ 0 (
  ECHO.
  ECHO ERROR: Tests failed!
  EXIT /B 1
)

ECHO.
ECHO ✓ Tests passed!
IF EXIST "%CoverageDir%\coverage.%Format%" (
  ECHO ✓ Coverage report generated: coverage\coverage.%Format%
) ELSE (
  ECHO ✓ Coverage report generated in: coverage\
)

REM Generate HTML report if requested
IF /I "%GenerateHtml%"=="true" (
  ECHO.
  ECHO ======================================
  ECHO Generating HTML coverage report...
  ECHO ======================================
  
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
    "-reports:%CoverageDir%\coverage.%Format%" ^
    "-targetdir:%HtmlReportDir%" ^
    "-reporttypes:Html;HtmlSummary;MarkdownSummary" ^
    "-verbosity:Warning"
  
  IF %ERRORLEVEL% EQU 0 (
    ECHO.
    ECHO ✓ HTML report generated: coverage\html\index.html
    ECHO.
    ECHO View the report:
    ECHO   START "%HtmlReportDir%\index.html"
  ) ELSE (
    ECHO ERROR: Failed to generate HTML report!
    EXIT /B 1
  )
)

ECHO.
ECHO ======================================
ECHO Coverage Summary
ECHO ======================================
ECHO Raw reports: %CoverageDir%\
ECHO HTML reports: %HtmlReportDir%\
ECHO.

ENDLOCAL
