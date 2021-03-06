/****** Object:  StoredProcedure [dbo].[SP_GetSchemeMasterRecords]    Script Date: 06/24/2009 12:04:51 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetSchemeMasterRecords]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_GetSchemeMasterRecords]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetSchemeMasterRecords]    Script Date: 06/24/2009 12:04:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetSchemeMasterRecords]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SP_GetSchemeMasterRecords]
(
@CurrentPage int =0 ,
@Flag char(2)
)
AS 
 BEGIN
  SET NOCOUNT ON;
 DECLARE @intStartRow int;  
 DECLARE @intEndRow int; 
 SET @intStartRow = (@CurrentPage -1) * 10 + 1;  
 SET @intEndRow = @CurrentPage * 10;
 
 if(@Flag=''C'')
 BEGIN
 With Entries as
 (
    SELECT
	A.PASP_SchemePlanCode as WERPCODE,
	A.PASP_SchemePlanName as NAME,
	dbo.FN_GetSchemeCode(A.PASP_SchemePlanCode, ''AMFI'') AS AMFICODE,
	dbo.FN_GetSchemeCode(A.PASP_SchemePlanCode, ''CAMS'') AS CAMSCODE,
	dbo.FN_GetSchemeCode(A.PASP_SchemePlanCode, ''KARVY'') AS KARVYCODE
	FROM 
	ProductAMCSchemePlan A 
 )
 Select Count(*) from Entries
 END
 
 ELSE IF(@Flag=''N'')
  BEGIN
 With Entries as
 (
    SELECT
	A.PASP_SchemePlanCode as WERPCODE,
	A.PASP_SchemePlanName as NAME,
	ROW_NUMBER() over (order by A.PASP_SchemePlanCode) as RowNum,
	dbo.FN_GetSchemeCode(A.PASP_SchemePlanCode, ''AMFI'') AS AMFICODE,
	dbo.FN_GetSchemeCode(A.PASP_SchemePlanCode, ''CAMS'') AS CAMSCODE,
	dbo.FN_GetSchemeCode(A.PASP_SchemePlanCode, ''KARVY'') AS KARVYCODE
	FROM 
	ProductAMCSchemePlan A 
 )
 Select * from Entries where RowNum BETWEEN @intStartRow AND @intEndRow;
 END
END' 
END
GO
