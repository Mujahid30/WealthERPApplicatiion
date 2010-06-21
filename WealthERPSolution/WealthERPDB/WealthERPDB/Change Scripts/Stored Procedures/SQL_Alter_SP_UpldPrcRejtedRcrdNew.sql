

ALTER PROCEDURE [dbo].[SP_UpldPrcRejtedRcrdNew] 
(
@StartDate Datetime, 
@EndDate Datetime,
@CurrentPage int ,
--@sortOrder varchar(4000),
@AssetGroup varchar(10),
@Exchange varchar(10) = null,
@Flag char(2)
)
AS

BEGIN
Declare @RejectedCount int
Declare @RejectedCout1 int
DECLARE @intStartRow int;  
DECLARE @intEndRow int; 
SET @RejectedCount=0
SET @RejectedCout1  = 0
SET @intStartRow = (@CurrentPage -1) * 10 + 1;  
SET @intEndRow = @CurrentPage * 10; 

Declare @RejectedTable Table
(
RejectedRecordCode varchar(25),
RejectedRecordName varchar(25),
Exchange varchar(25),
Reason varchar(50)
)


IF(@Flag='C')
BEGIN


 IF (@AssetGroup='MF' )
 BEGIN

		Select @RejectedCout1 = Count(*) from pcg1server.[MarketData_db].[dbo].[AMFIMarketData]
		where Scheme_Code NOT IN (Select PASC_AMC_ExternalCode from ProductAMCSchemeMapping)
		AND [Date]>= @startDate
		AND [Date]<=@EndDate
		SET @RejectedCount= @RejectedCount + @RejectedCout1;
		
		Select @RejectedCout1 = Count(*) from  ProductAMCSchemeMapping
		where  PASC_AMC_ExternalCode NOT IN (
												Select Scheme_Code  from  pcg1server.[MarketData_db].[dbo].[AMFIMarketData]
												where [Date]>= @startDate
												AND [Date]<=@EndDate
											 )
		AND PASC_AMC_ExternalType ='AMFI'									 
									 
		SET @RejectedCount= @RejectedCount + @RejectedCout1;
END
		
	
ELSE IF(@AssetGroup='Equity')
  BEGIN

--Counting Records from NSE which has no MAP in MappingTable
		if(@Exchange='NSE')
		BEGIN
			select @RejectedCount= count(*) from  pcg1server.[MarketData_db].[dbo].[NSEEquities] A
			where Symbol NOT IN (Select PESM_Identifier from ProductEquityScripMapping)
			AND [Date]>= @startDate
			AND [Date]<=@EndDate
			SET @RejectedCount= @RejectedCount + @RejectedCout1;
		END

--Counting Records from  BSE Tables which has no MAP in MappingTable
		if(@Exchange='BSE')
		BEGIN
			Select @RejectedCout1= Count(*) from pcg1server.[MarketData_db].[dbo].[BSEEquities]
			where Cast(SC_CODE AS Varchar(25)) NOT IN (Select PESM_Identifier from ProductEquityScripMapping)
			AND [Date]>= @startDate
			AND [Date]<=@EndDate
			SET @RejectedCount= @RejectedCount + @RejectedCout1;
		END

--Counting Records from Mapping Table which has No Mapping in either NSE OR BSE Table
		if(@Exchange ='WERP')
		BEGIN
				Select @RejectedCout1 = count(*) from ProductEquityScripMapping
				where PESM_IdentifierName in ('NSE', 'BSE')AND ( PESM_Identifier NOT IN (
											Select Symbol from pcg1server.[MarketData_db].[dbo].[NSEEquities]  
											where [Date]> =@startDate
											AND [Date]<=@EndDate
											 )
				AND  PESM_Identifier NOT IN(
											Select CAST(SC_CODE AS Varchar(25)) from pcg1server.[MarketData_db].[dbo].[BSEEquities]
											where [Date]>= @startDate
											AND [Date]<=@EndDate
											))						 
										 
				SET @RejectedCount= @RejectedCount + @RejectedCout1;
		END
    END


Select @RejectedCount

END

ELSE IF(@Flag='N')
  BEGIN
---- Getting Records

IF(@AssetGroup ='Equity')
  BEGIN
  
 With Entries AS 
 (
   Select PESM_Identifier as RejectedRecordCode ,
   PESM_IdentifierName as RejectedRecordName,
   'WERP' as Exchange ,
   'No Mapping in NSE OR BSE as Reason' as Reason 
   FROM ProductEquityScripMapping
   where PESM_IdentifierName in ('NSE','BSE') 
   AND (
		PESM_Identifier NOT IN (
							Select Symbol from pcg1server.[MarketData_db].[dbo].[NSEEquities]  
							where [Date] >= @startDate
							AND [Date]<= @EndDate
							 )
		AND PESM_Identifier NOT IN(
							Select CAST(SC_CODE AS Varchar(25)) from pcg1server.[MarketData_db].[dbo].[BSEEquities]
							where [Date] >= @startDate
							AND [Date]<= @EndDate
							) 
		)
			
	UNION	ALL
     Select  A.Symbol as RejectedRecordCode ,
     A.Symbol as RejectedRecordName ,
     'NSE' as Exchange ,
     'No NSE Mapping in WERP Mapping Table ' as Reason 
	  from  pcg1server.[MarketData_db].[dbo].[NSEEquities] A
	  where Symbol NOT IN (Select PESM_Identifier from ProductEquityScripMapping )
	  AND A.[Date] >= @startDate	
	  AND A.[Date]<= @EndDate	
	  
  Union ALL
    Select Cast(A.SC_CODE AS Varchar(25))  as RejectedRecordCode ,
    A.SC_Name as RejectedRecordName,
   'BSE' as Exchange, 
   'No BSE Mapping in WERP Mapping Table' as Reason
	from pcg1server.[MarketData_db].[dbo].[BSEEquities] A
	where Cast(A.SC_CODE AS Varchar(25)) NOT IN (Select PESM_Identifier from ProductEquityScripMapping)
	AND A.[Date] >= @startDate	
    AND A.[Date]<= @EndDate		  
)					

	select * from
	(select RejectedRecordCode, RejectedRecordName,Exchange,Reason ,(ROW_NUMBER() over (order by RejectedRecordCode)) as RowNum FROM Entries) i 
	where RowNum BETWEEN @intStartRow AND @intEndRow
      			
END
  
 ELSE IF(@AssetGroup='MF')
 BEGIN
	With Entries As
	(
	   Select A.Scheme_Code as RejectedRecordCode,Scheme_Name as RejectedRecordName,'AMFI'as Exchange , 'No Mapping in Mapping Table'as Reason
	   from pcg1server.[MarketData_db].[dbo].[AMFIMarketData] A
	   where Scheme_Code NOT IN (Select PASC_AMC_ExternalCode FROM ProductAMCSchemeMapping)
	   AND [Date]>= @startDate
	   AND [Date] < = @EndDate
	   
	   Union All
	   
	   Select A.PASP_SchemePlanCode as RejectedRecordCode,B.PASP_SchemePlanName as RejectedRecordName,'WERP' as Exchange ,'No Mapping in MF Table' as Reason
	  from  ProductAMCSchemeMapping A
	  inner join ProductAMCSchemePlan B on B.PASP_SchemePlanCode = A.PASP_SchemePlanCode
	  where  A.PASC_AMC_ExternalCode NOT IN (Select Scheme_Code  from  pcg1server.[MarketData_db].[dbo].[AMFIMarketData] where [Date]>= @startDate
											 AND [Date] < = @EndDate ) 
	  AND PASC_AMC_ExternalType ='AMFI'

	)
	

	select * from
	(select RejectedRecordCode, RejectedRecordName,Exchange,Reason ,(ROW_NUMBER() over (order by RejectedRecordCode)) as RowNum FROM Entries) i 
	where RowNum BETWEEN @intStartRow AND @intEndRow
  
  END

END
END 