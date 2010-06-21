ALTER PROCEDURE [dbo].[SP_GetProductAMCSchemePlanPrice]
(
@CurrentPage int =1,
@Flag char(2),
@SortOrder varchar(50)='PASP_SchemePlanName  ASC',
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

IF(@Flag='C')
BEGIN
	 Select Count(*) from ProductAMCSchemePlanPrice A
	 INNER JOIN ProductAMCSchemePlan B
	 ON A.PASP_SchemePlanCode=B.PASP_SchemePlanCode
	 WHERE A.PSP_Date >= @StartDate
	 AND A.PSP_Date <=@EndDate
 END
 
 ELSE IF(@Flag='N')
 BEGIN
 WITH Entries AS
 (
Select  B.PASP_SchemePlanName as SchemePlanName,A.PSP_NetAssetValue as NetAssetValue ,
		A.PSP_RepurchasePrice as RepurchasePrice  ,A.PSP_SalePrice as SalePrice ,
		CONVERT(VARCHAR , A.PSP_PostDate ,103) as PostDate,
		CONVERT(VARCHAR,A.PSP_Date,103) as [Date], 
		 ROW_NUMBER() over ( ORDER BY
							CASE WHEN @SortOrder = 'PASP_SchemePlanName DESC'
							THEN PASP_SchemePlanName END DESC,
							CASE WHEN @SortOrder = 'PASP_SchemePlanName ASC'
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
 