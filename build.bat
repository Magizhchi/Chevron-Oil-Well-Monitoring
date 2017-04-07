@echo off  
SET dotnet="C:\Program Files\dotnet\dotnet.exe"  
SET opencover="..\..\lib\OpenCover\4.6.519\tools\OpenCover.Console.exe"
SET reportgenerator="..\..\lib\ReportGenerator\2.5.2\tools\ReportGenerator.exe"

SET targetargs="test"  
SET filter="+[CoreNg2]* -[CoreNg2]*Program -[CoreNg2]*Startup -[*]CoreNg2.Migrations.* -[*]CoreNg2.Models.* -[xunit*]*" 
SET coveragefile="..\..\..\reports\XunitCoverageReport.xml"
SET coveragedir="..\..\..\reports\ReportGenerator Output"

REM Create a 'GeneratedReports' folder if it does not exist
if not exist "..\reports" mkdir "..\reports"
 
REM Run the tests against the targeted output
call :RunOpenCoverUnitTestMetrics
 
REM Generate the report output based on the test results
if %errorlevel% equ 0 (
 call :RunReportGeneratorOutput
)
exit /b %errorlevel%

:RunOpenCoverUnitTestMetrics
%dotnet% restore
cd test\CoreNg2.Tests
%opencover% -target:%dotnet% -targetargs:test -register:user -filter:%filter% -output:%coveragefile% -oldStyle
exit /b %errorlevel%

:RunReportGeneratorOutput
%reportgenerator% -targetdir:%coveragedir% -reports:%coveragefile% 
exit /b %errorlevel%