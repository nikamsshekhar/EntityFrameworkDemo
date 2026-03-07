# Code Coverage - Quick Start Guide

## Overview
This folder contains batch scripts for running code coverage and generating HTML reports for the EntityFramework Demo project.

## Files
- **run-coverage.bat** - Run tests with coverage collection and generate HTML reports
- **generate-html-report.bat** - Generate HTML reports from existing coverage data
- **COVERAGE.md** - Detailed documentation

## Quick Start

### Run Tests with Coverage and Generate HTML (Recommended)
```batch
run-coverage.bat cobertura true
```

This will:
1. ✓ Execute all unit tests
2. ✓ Collect code coverage data
3. ✓ Generate coverage reports in Cobertura format
4. ✓ Create interactive HTML report in `coverage/html/`

### View the HTML Report
```batch
START coverage\html\index.html
```

Or manually navigate to: `coverage/html/index.html` in your browser

## Usage

### run-coverage.bat
```batch
run-coverage.bat [format] [generateHtml]
```

**Parameters:**
- `format` - Report format: `cobertura`, `opencover`, `lcov`, `json` (default: `cobertura`)
- `generateHtml` - Generate HTML: `true` or `false` (default: `true`)

**Examples:**
```batch
run-coverage.bat                      REM Default: cobertura format, HTML enabled
run-coverage.bat json true            REM JSON format with HTML
run-coverage.bat opencover false      REM OpenCover format, no HTML
```

### generate-html-report.bat
```batch
generate-html-report.bat [format]
```

**Parameters:**
- `format` - Source report format: `cobertura`, `opencover`, `lcov`, `json` (default: `cobertura`)

**Examples:**
```batch
generate-html-report.bat              REM Generate HTML from cobertura.xml
generate-html-report.bat json         REM Generate HTML from coverage.json
```

## Report Locations

**Coverage Data:**
- `coverage/coverage.cobertura.xml` - Raw Cobertura format
- `coverage/coverage.json` - JSON format
- `coverage/coverage.lcov` - LCOV format
- `coverage/coverage.opencover.xml` - OpenCover format

**HTML Reports:**
- `coverage/html/index.html` - Main interactive report
- `coverage/html/summary.html` - Summary page
- `coverage/html/Summary.md` - Markdown summary
- Individual coverage files for each code unit

## Features

### HTML Report Includes
- Overall line, branch, and method coverage percentages
- Per-file coverage breakdown with color-coded highlighting
- Interactive file browser and code viewer
- Coverage metrics by class and method
- Risk analysis based on code complexity
- Mobile-responsive design
- Dark mode support

## Requirements

- .NET 8.0 SDK
- ReportGenerator (auto-installed on first run)

## Troubleshooting

### Scripts won't run
- Ensure you're in the `EntityFrameworkCore.UnitTests` directory
- Run from Command Prompt (cmd.exe) or PowerShell
- Make sure `.bat` files have execute permissions

### Coverage data not generated
- Check that tests are passing: `dotnet test`
- Verify `.NET` is in your system PATH: `dotnet --version`
- Clear old coverage: `rmdir coverage /s /q` then retry

### HTML report is empty
- Ensure coverage data exists: `coverage\coverage.cobertura.xml`
- Run `generate-html-report.bat` to regenerate
- Verify ReportGenerator is installed: `reportgenerator --version`

### ReportGenerator installation fails
```batch
dotnet tool install -g dotnet-reportgenerator-globaltool
dotnet tool update -g dotnet-reportgenerator-globaltool
```

## For More Information
See **COVERAGE.md** for complete documentation including:
- Integration with CI/CD pipelines
- Coverage threshold configuration
- Integration with SonarQube and Codecov
- Advanced usage and customization

## Examples

### Generate coverage with 80% threshold
```batch
dotnet test /p:CollectCoverage=true /p:Threshold=80
```

### Regenerate HTML only (no test execution)
```batch
generate-html-report.bat cobertura
```

### Generate multiple formats
```batch
run-coverage.bat cobertura true
run-coverage.bat json true
run-coverage.bat opencover true
```
