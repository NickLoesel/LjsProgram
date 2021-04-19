echo off

rem batch file to run a script to create a db
rem 9/10/2020

sqlcmd -S localhost -E -i Ljs_DB.sql
rem sqlcmd -S localhost\sqlexpress -E -i Ljs_DB.sql
rem sqlcmd -S localhost\mssqlserver -E -i Ljs_DB.sql


Echo .
Echo if no error messages appear DB was created
Pause
