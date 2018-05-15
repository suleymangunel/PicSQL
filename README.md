# PicSQL
PicSQL saves written for saving image files to databases. Primarily idea is limited with image files but nature of binary processes 
program can be use for save and get any file types. 

PicSQL written with Visual Studio 2015 with C#.NET 4.5.

When used it with MySQL i saw this DB limits packet size, and you maybe you need to change it. If you are in this situation;
you can change this value in "my.ini" located on "C:\ProgramData\MySQL\MySQL Server 5.7" (5.7 changeable according to your DB ver.) and 
key is "max_allowed_packet". I setted it to 100M, you can set it according to your requirements or your network and/or internet speed.
This also defines your maximum uploadable file size.

PicSQL need SQLite and MySQL references,you can add its from Nuget in Visual Studio.

Working with DB's you have set attributes listed below.

   <add key="dbDataSource_MSSQL" value="" /> MS SQL Server's IP Address/URL
   <add key="dbInitialCatalog_MSSQL" value="" /> MS SQL DB name
   <add key="dbUserID_MSSQL" value="" />
   <add key="dbPassword_MSSQL" value="" />
   <add key="dbImageTable_MSSQL" value="Image" />
   <add key="dbDataSource_SQLite" value="" /> SQLite file location & filename
   <add key="dbInitialCatalog_SQLite" value="" />
   <add key="dbPassword_SQLite" value="" />
   <add key="dbImageTable_SQLite" value="IMAGE" />
   <add key="dbServer_MySQL" value="" /> MySQL's IP Address/URL
   <add key="dbDatabase_MySQL" value="" /> MySQL DB name
   <add key="dbUserID_MySQL" value="" />
   <add key="dbPassword_MySQL" value="" />
   <add key="dbImageTable_MySQL" value="image" />
