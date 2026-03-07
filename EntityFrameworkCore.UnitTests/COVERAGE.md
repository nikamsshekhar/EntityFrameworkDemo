# Code Coverage Configuration

This document describes the code coverage setup for the EntityFrameworkCore.UnitTests project.

## Overview

Code coverage is enabled using:

- **Coverlet** - Modern code coverage tool for .NET
  - `coverlet.collector` - Collects coverage data during test execution
  - `coverlet.msbuild` - MSBuild integration for advanced coverage scenarios
- **ReportGenerator** - Converts coverage reports to HTML, Markdown, and other formats
  - `dotnet-reportgenerator-globaltool` - Global tool for HTML report generation
- **.coverletrc.json** - Configuration file for coverage settings

## Running Tests with Coverage

### Option 1: Using Batch File (Recommended - Windows Command Line)
```batch
cd EntityFrameworkCore.UnitTests
run-coverage.bat cobertura true
```

This batch file will:
1. Run all tests with coverage collection
2. Generate coverage reports in the specified format (default: `cobertura`)
3. **Automatically generate HTML reports** in `coverage/html/`
4. Display the coverage summary

**Usage:**
```batch
run-coverage.bat [format] [generateHtml]
```

**Parameters:**
- `format` - Report format: `cobertura`, `opencover`, `lcov`, `json` (default: `cobertura`)
- `generateHtml` - Generate HTML reports: `true` or `false` (default: `true`)

**Examples:**
```batch
run-coverage.bat                          REM Uses defaults
run-coverage.bat cobertura true          REM Explicit format and HTML
run-coverage.bat opencover false         REM Generate report only, no HTML
```

### Option 2: Generate HTML Reports from Existing Data (Command Line)
If you already have coverage data and just want to regenerate the HTML report:

```batch
cd EntityFrameworkCore.UnitTests
generate-html-report.bat cobertura
```

This generates:
- `coverage/html/index.html` - Main coverage report
- `coverage/html/summary.md` - Markdown summary

**Usage:**
```batch
generate-html-report.bat [format]
```

**Parameters:**
- `format` - Source report format: `cobertura`, `opencover`, `lcov`, `json` (default: `cobertura`)

### Option 3: Using dotnet CLI

**Basic coverage report:**
```bash
dotnet test /p:CollectCoverage=true
```

**Generate specific format:**
```bash
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura
```

**Output to specific directory:**
```bash
dotnet test /p:CollectCoverage=true /p:CoverletOutput=./coverage/
```

**Set minimum coverage threshold:**
```bash
dotnet test /p:CollectCoverage=true /p:Threshold=75
```

### Option 4: Full Command with All Options
```bash
dotnet test EntityFrameworkCore.UnitTests.csproj `
  /p:CollectCoverage=true `
  /p:CoverletOutput=./coverage/ `
  /p:CoverletOutputFormat=cobertura `
  /p:Threshold=75 `
  /p:ThresholdType=line
```

## Viewing HTML Reports

### Automatic Generation (Recommended)
The recommended `run-coverage.bat` batch file automatically generates HTML reports:

```batch
run-coverage.bat cobertura true
```

The HTML report will be generated at: `coverage/html/index.html`

### Manual Generation with ReportGenerator
Generate HTML reports from existing coverage data:

```batch
REM Generate from Cobertura report
reportgenerator "-reports:coverage\coverage.cobertura.xml" "-targetdir:coverage\html" "-reporttypes:Html;HtmlSummary"

REM Or use the helper batch file
generate-html-report.bat cobertura
```

### Viewing the Report
Open the HTML report in your browser:

```batch
START "coverage/html/index.html"
```

Or manually navigate to: `./coverage/html/index.html`

### Report Contents
The HTML report includes:
- **Coverage Summary** - Overall line, branch, and method coverage
- **File-level Coverage** - Per-file breakdown with highlighted code
- **Coverage Details** - Covered and uncovered lines
- **Risk Analysis** - Code complexity vs. coverage heat map
- **Trending** (if available) - Coverage history over time

## Coverage Report Locations

After running tests with coverage, reports are generated in the `coverage/` folder:

**Coverage Data Reports (Raw Format):**
- `coverage.cobertura.xml` - Cobertura XML format (recommended for CI/CD)
- `coverage.opencover.xml` - OpenCover XML format
- `coverage.json` - JSON format with detailed metrics
- `coverage.lcov` - LCOV format

**HTML Reports (Human-Readable):**
- `coverage/html/index.html` - Main coverage report with interactive visualizations
- `coverage/html/summary.md` - Markdown summary of coverage metrics
- `coverage/html/report.html` - Detailed coverage breakdown by file

## ReportGenerator Setup

**ReportGenerator** converts coverage reports (XML/JSON) into human-readable HTML reports with interactive visualizations.

### Installation

ReportGenerator is installed as a global .NET tool:

```bash
dotnet tool install -g dotnet-reportgenerator-globaltool
```

### Usage

The recommended way is using the helper script:

```powershell
./run-coverage.ps1           # Runs tests + generates HTML automatically
./generate-html-report.ps1   # Generates HTML from existing coverage data
```

Or manually with ReportGenerator:

```bash
reportgenerator \
  "-reports:coverage/coverage.cobertura.xml" \
  "-targetdir:coverage/html" \
  "-reporttypes:Html;HtmlSummary"
```

### Report Features

The generated HTML report provides:
- ✓ **Line, branch, and method coverage** percentages
- ✓ **Per-file coverage details** with syntax highlighting
- ✓ **Covered vs. uncovered code** visualization
- ✓ **Risk analysis** and complexity metrics
- ✓ **Sortable coverage tables** by file/folder
- ✓ **Coverage history** tracking (if configured)
- ✓ **Mobile-responsive design** for any device
- ✓ **Dark mode** support

### Report Types

Available in the configuration:
- `Html` - Interactive HTML with full details
- `HtmlSummary` - Quick summary page
- `MarkdownSummary` - Markdown for README files
- `Xml` - Detailed XML export
- `JsonSummary` - JSON metrics export

## Configuration (.coverletrc.json)

The `.coverletrc.json` file controls what gets measured:

### Include Patterns
```json
"include": [
  "[EntityFrameworkCore.Domain]*",
  "[EntityFrameworkCore.Repository]*"
]
```
Measures code coverage for Domain and Repository projects.

### Exclude Patterns
```json
"exclude": [
  "[EntityFrameworkCore.UnitTests]*",
  "[xunit*]*"
]
```
Excludes test project and xUnit framework from coverage.

### Exclude by Attribute
```json
"excludeByAttribute": [
  "Obsolete",
  "GeneratedCodeAttribute",
  "CompilerGeneratedAttribute"
]
```
Ignores obsolete and compiler-generated code.

### Output Formats
```json
"format": [
  "json",
  "lcov",
  "opencover",
  "cobertura"
]
```
Generates reports in multiple formats for flexibility.

## Integration with CI/CD

### Azure Pipelines Example
```yaml
- task: DotNetCoreCLI@2
  inputs:
    command: test
    projects: '**/EntityFrameworkCore.UnitTests.csproj'
    arguments: '/p:CollectCoverage=true /p:CoverletOutputFormat=cobertura /p:CoverletOutput=$(Agent.TempDirectory)/coverage'

- task: PublishCodeCoverageResults@1
  inputs:
    codeCoverageTool: Cobertura
    summaryFileLocation: $(Agent.TempDirectory)/coverage/coverage.cobertura.xml
```

### GitHub Actions Example
```yaml
- name: Run tests with coverage
  run: dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=lcov

- name: Upload coverage to Codecov
  uses: codecov/codecov-action@v3
  with:
    files: ./coverage.lcov
```

## Coverage Thresholds

Set minimum coverage thresholds to enforce code quality:

```bash
# Require 80% line coverage
dotnet test /p:CollectCoverage=true /p:Threshold=80 /p:ThresholdType=line

# Require 75% branch coverage
dotnet test /p:CollectCoverage=true /p:Threshold=75 /p:ThresholdType=branch

# Require 70% method coverage
dotnet test /p:CollectCoverage=true /p:Threshold=70 /p:ThresholdType=method
```

## Analyzing Coverage Reports

### Using SonarQube
1. Generate cobertura report: `/p:CoverletOutputFormat=cobertura`
2. Upload to SonarQube with: `sonar-scanner -Dsonar.coverageReportPaths=./coverage/coverage.cobertura.xml`

### Using Codecov
1. Generate lcov report: `/p:CoverletOutputFormat=lcov`
2. Upload to Codecov

### Local Analysis
1. Generate JSON report: `/p:CoverletOutputFormat=json`
2. Review `coverage.json` for detailed metrics per file

## Best Practices

1. **Run coverage before committing:** Ensure new code maintains or improves coverage
2. **Set realistic thresholds:** Start with 70-80% and gradually increase
3. **Focus on critical paths:** Prioritize coverage for domain and repository logic
4. **Exclude generated code:** Already configured in `.coverletrc.json`
5. **Review trends:** Track coverage reports over time in CI/CD

## Troubleshooting

### Coverage report not generated
- Ensure at least one test passes
- Check that the coverage folder exists or is writable
- Verify `/p:CollectCoverage=true` is set

### Low coverage numbers
- Check that included projects are correct in `.coverletrc.json`
- Verify tests are actually running: `dotnet test -v detailed`
- Add missing unit tests for untested code paths

### HTML report not generated
- Verify ReportGenerator is installed: `reportgenerator --version`
- If not installed: `dotnet tool install -g dotnet-reportgenerator-globaltool`
- Check that coverage data exists before running report generation
- Ensure the coverage folder is writable
- Run with explicit command: `reportgenerator "-reports:coverage\coverage.cobertura.xml" "-targetdir:coverage\html" "-reporttypes:Html;HtmlSummary"`

### ReportGenerator not found
```batch
REM Install globally
dotnet tool install -g dotnet-reportgenerator-globaltool

REM Update if already installed
dotnet tool update -g dotnet-reportgenerator-globaltool

REM Verify installation
reportgenerator --version
```

### Script execution issues (Batch files)

**If batch files don't execute:**
1. Ensure you're in the correct directory: `cd EntityFrameworkCore.UnitTests`
2. Verify dotnet is in PATH: `dotnet --version`
3. Run from Command Prompt or PowerShell with administrative privileges
4. Check file permissions on .bat files

**Common fixes:**
```batch
REM Run coverage directly with dotnet
dotnet test /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura

REM Generate HTML manually
reportgenerator "-reports:coverage\coverage.cobertura.xml" "-targetdir:coverage\html" "-reporttypes:Html"
```

## Additional Resources

- [Coverlet Official Documentation](https://github.com/coverlet-coverage/coverlet)
- [Cobertura Format](https://cobertura.github.io/cobertura/)
- [Code Coverage Best Practices](https://en.wikipedia.org/wiki/Code_coverage)
