ALTER PROCEDURE [dbo].[SP_GetProductEquityPrice]
(
@CurrentPage int =1,
@Flag char(2),
@SortOrder varchar(50)='PEM_CompanyName ASC',
@StartDate datetime,
@EndDate datetime


)
AS
BEGIN
SET NOCOUNT ON 
DECLARE @intStartRow int; 
DECLARE @intEndRow int;
SET @intStartRow = (@CurrentPage -1) * 10 + 1;  
SET @intEndRow = @CurrentPage * 10;

IF(@Flag='C')
BEGIN
	 Select Count(*) from ProductEquityPrice A
	 INNER JOIN ProductEquityMaster B
	 ON A.PEM_ScripCode=B.PEM_ScripCode
	 WHERE A.PEP_Date >= @StartDate
	 AND A.PEP_Date <=@EndDate
 END
 
 ELSE IF(@Flag='N')
 BEGIN
 WITH Entries AS
 (
Select  B.PEM_CompanyName as CompanyName , 
		A.PEM_Exchange as Exchange,A.PEP_Series as Series,
		 A.PEP_OpenPrice as OpenPrice,
         A.PEP_HighPrice as HighPrice,A.PEP_LowPrice as LowPrice,A.PEP_ClosePrice as ClosePrice,
         A.PEP_LastPrice as LastPrice,
		 A.PEP_PreviousClose as PreviousClose,A.PEP_TotalTradeQuantity as TotalTradeQuantity ,
		 A.PEP_TotalTradeValue as TotalTradeValue ,
		 A.PEP_NoOfTrades as NoOfTrades ,
		 CONVERT(VARCHAR,PEP_Date,103) as [Date],
		 ROW_NUMBER() over ( ORDER BY
							CASE WHEN @SortOrder = 'PEM_CompanyName DESC'
							THEN PEM_CompanyName END DESC,
							CASE WHEN @SortOrder = 'PEM_CompanyName ASC'
							THEN PEM_CompanyName END ASC ) as RowNum 
		 FROM ProductEquityPrice A
		 INNER JOIN ProductEquityMaster B
		 ON A.PEM_ScripCode=B.PEM_ScripCode
		 WHERE A.PEP_Date >= @StartDate
		 AND A.PEP_Date <=@EndDate
		 
 )
  Select * from Entries where RowNum BETWEEN @intStartRow AND @intEndRow


 END
 SET NOCOUNT OFF ; 
END 
 