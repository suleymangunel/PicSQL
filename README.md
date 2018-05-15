# PicSQL
PicSQL saves written for saving image files to databases. Primarily idea is limited with image files but nature of binary processes 
program can be use for save and get any file types. 

PicSQL written with Visual Studio 2015 with C#.NET 4.5.

When used it with MySQL i saw this DB limits packet size, and you maybe you need to change it. If you are in this situation;
you can change this value in "my.ini" located on "C:\ProgramData\MySQL\MySQL Server 5.7" (5.7 changeable according to your DB ver.) and 
key is "max_allowed_packet". I setted it to 100M, you can set it according to your requirements or your network and/or internet speed.
This also defines your maximum uploadable file size.

PicSQL need SQLite and MySQL references,you can add its from Nuget in Visual Studio.

Working with DB's you have set value of keys listed below.

"dbDataSource_MSSQL" value=""       MS SQL Server's IP Address/URL <br/>
"dbInitialCatalog_MSSQL" value=""   MS SQL DB name.
"dbUserID_MSSQL" value=""
"dbPassword_MSSQL" value=""

"dbImageTable_MSSQL" value="Image"
"dbDataSource_SQLite" value=""      SQLite file location & filename.
"dbInitialCatalog_SQLite" value=""
"dbPassword_SQLite" value=""
"dbImageTable_SQLite" value="IMAGE"
"dbServer_MySQL" value=""           MySQL's IP Address/URL.
"dbDatabase_MySQL" value=""         MySQL DB name.
"dbUserID_MySQL" value=""
"dbPassword_MySQL" value=""
"dbImageTable_MySQL" value="image"
