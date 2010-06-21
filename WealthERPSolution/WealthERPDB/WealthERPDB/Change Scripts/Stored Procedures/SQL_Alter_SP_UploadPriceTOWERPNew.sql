
-- add date parameter.

ALTER PROCEDURE [dbo].[SP_UploadPriceTOWERPNew] 
(
@StartDate Datetime, 
@EndDate Datetime,
@AssetGroup Varchar(10),
@Exchange Varchar(10)
)
AS

BEGIN
	
Declare @UploadCount1 int
Declare @UploadCount2 int
Set @UploadCount1 =0	
Set @UploadCount2 =0
Declare @Date Datetime;	
Declare  
  @bTran AS INT,      
  @lErrCode AS INT
	

	
SET NOCOUNT ON;
			-- Begin Tran
		If (@@Trancount = 0)  
		 Begin  
		  Set @bTran = 1  
		  Begin Transaction  
		End 
 
IF(@AssetGroup='Equity' and @Exchange = 'NSE' )

   BEGIN		
---- NSE DATA 
--Selecting proper date range .	
	 Select @Date = max(PEP_Date) from ProductEquityPrice

	if(Datediff(day,@startDate, @Date)>=0)
	 Select @StartDate = DATEADD(day,1,@Date)
	 
	if(Datediff(day,GETDATE(),@EndDate)>0)
	 SET @EndDate = GetDate()	
	 
   BEGIN


 Insert INTO ProductEquityPrice
   (
    PEM_ScripCode,
    PEM_Exchange,
    PEP_Series,
    PEP_OpenPrice,
    PEP_HighPrice,
    PEP_LowPrice,
    PEP_ClosePrice,
    PEP_LastPrice,
    PEP_PreviousClose,
    PEP_TotalTradeQuantity,
    PEP_TotalTradeValue,
    PEP_Date
   )
 SELECT
 C.PEM_ScripCode,
 'NSE',
 A.Series,
 A.Open_Price,
 A.High_Price,
 A.Low_Price,
 A.Close_Price,
 A.Last_Price, 
 A.Previous_Close, 
 A.Total_Trade_Qty,
 A.Total_Trade_Value,
 A.[Date]
 
FROM pcg1server.[MarketData_db].[dbo].[NSEEquities] A
inner join ProductEquityScripMapping C on A.Symbol=C.PESM_Identifier
inner join [ProductEquityMaster] B on  C.PEM_ScripCode=B.PEM_ScripCode
where   C.PESM_IdentifierName='NSE'
		AND A.[Date]>=@StartDate
		AND A.[Date]<=@EndDate
Select @UploadCount1 = @@rowcount; 	
END

END
--All BSE ScripCode minus NSE ScripCode
ELSE IF ( @AssetGroup='Equity' and @Exchange = 'BSE' )
    BEGIN
     Select @Date = max(PEP_Date) from ProductEquityPrice

	if(Datediff(day,@startDate, @Date)>=0)
	 Select @StartDate = DATEADD(day,1,@Date)
	 
	if(Datediff(day,GETDATE(),@EndDate)>0)
	 SET @EndDate = GetDate()
	 
BEGIN
 Insert INTO ProductEquityPrice
   (
    PEM_ScripCode,
    PEM_Exchange,
    PEP_OpenPrice,
    PEP_HighPrice,
    PEP_LowPrice,
    PEP_ClosePrice,
    PEP_LastPrice,
    PEP_PreviousClose,
    PEP_TotalTradeQuantity,
    PEP_Date
   )
   select 
      A.PEM_ScripCode,
     'BSE',
      B.OPEN_VALUE, 
      B.HIGH_VALUE,
      B.LOW_VALUE,
      B.CLOSE_VALUE,
	  B.LAST_VALUE,
      B.PREVCLOSE_VALUE,
      B.NO_TRADES_VALUE,
      B.[Date]
      FROM [ProductEquityMaster] A
      inner join  ProductEquityScripMapping C on A.PEM_ScripCode=C.PEM_ScripCode
       inner join  pcg1server.[MarketData_db].[dbo].[BSEEquities] B on B.SC_CODE=C.PESM_Identifier 
      WHERE  C.PESM_IdentifierName='BSE'
      	AND B.[Date]>=@StartDate
		AND B.[Date]<=@EndDate
      AND A.PEM_ScripCode NOT IN
								(
								 SELECT C.PEM_ScripCode
								 FROM pcg1server.[MarketData_db].[dbo].[NSEEquities] A, [ProductEquityMaster] B,ProductEquityScripMapping C
								 WHERE A.Symbol=C.PESM_Identifier
								 AND C.PEM_ScripCode=B.PEM_ScripCode 
								 AND  C.PESM_IdentifierName='NSE' 
								 )
    						 

  Select @UploadCount2 = @@rowcount 
  SET @UploadCount1=@UploadCount1 +@UploadCount2 ;
  END
  END
  
IF(@AssetGroup='MF')
  BEGIN
 
   Select @Date = max(PSP_Date) from ProductAMCSchemePlanPrice

	if(Datediff(day,@startDate, @Date)>=0)
	 Select @StartDate = DATEADD(day,1,@Date)
	 
	if(Datediff(day,GETDATE(),@EndDate)>0)
	 SET @EndDate = GetDate()	
	
    BEGIN

		INSERT INTO ProductAMCSchemePlanPrice
		(
		 PASP_SchemePlanCode,
		 PSP_NetAssetValue,
		 PSP_RepurchasePrice,
		 PSP_SalePrice,
		 PSP_PostDate,
		 PSP_Date
		 )
		SELECT  distinct
		   C.PASP_SchemePlanCode,
		   A.Net_Asset_Value,
		   A.Repurchase_Price,
		   A.Sale_Price, 
		   A.Post_Date,
		   A.[Date]
			FROM pcg1server.[MarketData_db].[dbo].[AMFIMarketData] A
			inner join ProductAMCSchemeMapping C on A.Scheme_Code=C.PASC_AMC_ExternalCode 
			WHERE  C.PASC_AMC_ExternalType = 'AMFI'  
			AND A.[Date] >= @StartDate
			AND A.[Date]<= @EndDate

 Select @UploadCount2 = @@rowcount 
 SET @UploadCount1=@UploadCount1 +@UploadCount2 ;



 END
 END
 
Select @UploadCount1 as [RowCount];

If (@@Error <> 0)                    
	Begin                    
	  Set @lErrCode = 1001 -- This is an error code set by the application     
	  Goto Error                    
	End  

		Success:      
		 If (@bTran = 1 And @@Trancount > 0)      
		 Begin                                    
		  Commit Tran      
		 End      
		 Return 0      
      
		Goto Done      
      
		Error:      
		 If (@bTran = 1 And @@Trancount > 0)      
		 Begin      
		  Rollback Transaction      
		 End      
		 Return @lErrCode      

	       
		Done:     
 
SET NOCOUNT OFF;
END
 