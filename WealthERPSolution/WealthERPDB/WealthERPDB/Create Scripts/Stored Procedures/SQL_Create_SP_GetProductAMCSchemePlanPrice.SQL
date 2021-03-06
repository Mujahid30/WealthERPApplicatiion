/****** Object:  StoredProcedure [dbo].[SP_GetProductAMCSchemePlanPrice]    Script Date: 06/24/2009 12:03:52 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetProductAMCSchemePlanPrice]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_GetProductAMCSchemePlanPrice]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetProductAMCSchemePlanPrice]    Script Date: 06/24/2009 12:03:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetProductAMCSchemePlanPrice]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[SP_GetProductAMCSchemePlanPrice]
(
@CurrentPage int =1,
@Flag char(2),
@SortOrder varchar(50)=''PASP_SchemePlanName  ASC'',
@StartDate datetime,
@EndDate datetime


)
AS
BEGIN
SET NOCOUNT ON 
DECLARE @intStartRow int; 
--DECLARE @sql varchar(max); 
DECLARE @intEndRow int;
SET @intStartRow = (@CurrentPage -1) * 10 + 1;  
SET @intEndRow = @CurrentPage * 10;

IF(@Flag=''C'')
BEGIN
	 Select Count(*) from ProductAMCSchemePlanPrice A
	 INNER JOIN ProductAMCSchemePlan B
	 ON A.PASP_SchemePlanCode=B.PASP_SchemePlanCode
	 WHERE A.PSP_Date >= @StartDate
	 AND A.PSP_Date <=@EndDate
 END
 
 ELSE IF(@Flag=''N'')
 BEGIN
 WITH Entries AS
 (
Select  B.PASP_SchemePlanName as SchemePlanName,A.PSP_NetAssetValue as NetAssetValue ,
		A.PSP_RepurchasePrice as RepurchasePrice  ,A.PSP_SalePrice as SalePrice ,
		CONVERT(VARCHAR , A.PSP_PostDate ,103) as PostDate,
		CONVERT(VARCHAR,A.PSP_Date,103) as [Date], 
		 ROW_NUMBER() over ( ORDER BY
							CASE WHEN @SortOrder = ''PASP_SchemePlanName DESC''
							THEN PASP_SchemePlanName END DESC,
							CASE WHEN @SortOrder = ''PASP_SchemePlanName ASC''
							THEN PASP_SchemePlanName END ASC ) as RowNum 
	 from ProductAMCSchemePlanPrice A
	 INNER JOIN ProductAMCSchemePlan B
	 ON A.PASP_SchemePlanCode=B.PASP_SchemePlanCode
	 WHERE A.PSP_Date >= @StartDate
	 AND A.PSP_Date <=@EndDate
		 
 )
  Select * from Entries where RowNum BETWEEN @intStartRow AND @intEndRow


 END
 SET NOCOUNT OFF ; 
END 
' 
END
GO
