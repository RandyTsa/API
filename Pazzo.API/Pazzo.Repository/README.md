請使用以下指令建立DataseModel, 並根據實際需求替換對應參數
{Server}: DB Server IP
{Database}: Database Name
{UserId}: DB登入帳號
{Password}: DB登入密碼
{RepositoryProjectName}: Repository專案名稱, ex. xxxx.Repository
{ConextName}: 自定義DbConext Class名稱
{TableName(,TableName2)}: 要匯入的TableName, 若有多個請用逗號隔開, ex. Table1,Table2

``` 以 schema 切
Scaffold-DbContext -Verbose "data source=(localdb)\MSSQLLocalDB;initial catalog=pazzo;persist security info=True;encrypt=true;trustServerCertificate=true;MultipleActiveResultSets=True;App=EntityFramework" Microsoft.EntityFrameworkCore.SqlServer -DataAnnotations -NoOnConfiguring -Force -Project Pazzo.Repository -OutputDir Models -Context PazzoContext -ContextDir "Contexts" -Schema pazzo

``` 以 table 切
Scaffold-DbContext -Verbose "data source=(localdb)\MSSQLLocalDB;initial catalog=pazzo;integrated security=True;MultipleActiveResultSets=True;App=EntityFramework" Microsoft.EntityFrameworkCore.SqlServer -DataAnnotations -NoOnConfiguring -Force -Project Pazzo.Repository -OutputDir Models -Context PazzoContext -ContextDir "Contexts" -Table Member
