/****** Object:  StoredProcedure [dbo].[SP_GetScripMasterRecords]    Script Date: 06/24/2009 12:04:58 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetScripMasterRecords]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_GetScripMasterRecords]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetScripMasterRecords]    Script Date: 06/24/2009 12:04:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetScripMasterRecords]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SP_GetScripMasterRecords]
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
WITH ENTRIES AS
	  (
	SELECT
		A.PEM_ScripCode as WERPCODE,
		A.PEM_CompanyName as NAME,
		CONVERT(VARCHAR,A.PEM_Incorporation,103) as IncorporationDate,
		ROW_NUMBER() over (order by A.PEM_ScripCode) as RowNum,
		dbo.FN_GetExchangeCode(A.PEM_ScripCode, ''BSE'') AS BSECODE,
		dbo.FN_GetExchangeCode(A.PEM_ScripCode, ''NSE'') AS NSECODE,
		dbo.FN_GetExchangeCode(A.PEM_ScripCode, ''CERC'') AS CERCCODE,
		dbo.FN_GetExchangeCode(A.PEM_ScripCode, ''ISIN'') AS ISINNO
	FROM 
	ProductEquityMaster A
)
	Select count(*) from Entries
 END
 
 ELSE IF(@Flag=''N'')
 BEGIN
  WITH ENTRIES AS
  (
SELECT
	A.PEM_ScripCode as WERPCODE,
	A.PEM_CompanyName as NAME,
	CONVERT(VARCHAR,A.PEM_Incorporation,103) as IncorporationDate,
	ROW_NUMBER() over (order by A.PEM_ScripCode) as RowNum,
	dbo.FN_GetExchangeCode(A.PEM_ScripCode, ''BSE'') AS BSECODE,
	dbo.FN_GetExchangeCode(A.PEM_ScripCode, ''NSE'') AS NSECODE,
	dbo.FN_GetExchangeCode(A.PEM_ScripCode, ''CERC'') AS CERCCODE,
	dbo.FN_GetExchangeCode(A.PEM_ScripCode, ''ISIN'') AS ISINNO
FROM 
ProductEquityMaster A
)
Select * from ENTRIES where RowNum BETWEEN @intStartRow AND @intEndRow;
END

SET NOCOUNT OFF;
END
' 
END
GO
