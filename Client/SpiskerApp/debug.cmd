npm install 
RMDIR wwwroot\debug\app /S /Q
RMDIR wwwroot\debug\testing /S /Q
MKDIR wwwroot\debug\app
MKDIR wwwroot\debug\testing
xcopy "ClientApp\app\*.*" "wwwroot\debug\app" /S /Y
xcopy "ClientApp\testing\*.*" "wwwroot\debug\testing" /S /Y
tsc -p wwwroot/debug/tsconfig.json