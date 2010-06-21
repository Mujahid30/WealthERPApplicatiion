

ALTER PROCEDURE [dbo].[SP_UpldPrcRejtedRcrd] 
(
@StartDate Datetime, 
@AssetGroup varchar(10),
@Exchange varchar(10),
@Flag char(2)
)
AS

BEGIN
Declare @RejectedCount int
Declare @RejectedCout1 int
SET @RejectedCount=0
SET @RejectedCout1  = 0
Declare @RejectedTable Table
(
RejectedRecordCode varchar(25),
RejectedRecordName varchar(25),
Exchange varchar(25),
Reason varchar(50)
)

IF(@Flag='C')
  
  BEGIN

	IF(@AssetGroup='Equity')

	BEGIN

--Counting Records from NSE which has no MAP in MappingTable
if(@Exchange='NSE')
BEGIN
select @RejectedCount= count(*) from  pcg1server.[MarketData_db].[dbo].[NSEEquities] A
where Symbol NOT IN (Select PESM_Identifier from ProductEquityScripMapping)
AND [Date]= @startDate
SET @RejectedCount= @RejectedCount + @RejectedCout1;
END

--Counting Records from  BSE Tables which has no MAP in MappingTable
if(@Exchange='BSE')
BEGIN
Select @RejectedCout1= Count(*) from pcg1server.[MarketData_db].[dbo].[BSEEquities]
where Cast(SC_CODE AS Varchar(25)) NOT IN (Select PESM_Identifier from ProductEquityScripMapping)
AND [Date]= @startDate

SET @RejectedCount= @RejectedCount + @RejectedCout1;
END

--Counting Records from Mapping Table which has No Mapping in either NSE OR BSE Table
if(@Exchange ='WERP')
BEGIN
Select @RejectedCout1 = count(*) from ProductEquityScripMapping
where PESM_IdentifierName in ('NSE', 'BSE')AND ( PESM_Identifier NOT IN (
							Select Symbol from pcg1server.[MarketData_db].[dbo].[NSEEquities]  
							where [Date] =@startDate
							 )
AND  PESM_Identifier NOT IN(
							Select CAST(SC_CODE AS Varchar(25)) from pcg1server.[MarketData_db].[dbo].[BSEEquities]
							where [Date]= @startDate
							))						 
						 
SET @RejectedCount= @RejectedCount + @RejectedCout1;
END
END

ELSE IF (@AssetGroup='MF' and @Exchange='MF')
 
 BEGIN

Select @RejectedCout1 = Count(*) from pcg1server.[MarketData_db].[dbo].[AMFIMarketData]
where Scheme_Code NOT IN (Select PASC_AMC_ExternalCode from [wealtherpV1].[dbo].ProductAMCSchemeMapping)
AND [Date]= @startDate

SET @RejectedCount= @RejectedCount + @RejectedCout1;

Select @RejectedCout1 = Count(*) from  [wealtherpV1].[dbo].ProductAMCSchemeMapping
where  PASC_AMC_ExternalCode NOT IN (
										Select Scheme_Code  from  pcg1server.[MarketData_db].[dbo].[AMFIMarketData]
										where [Date]= @startDate
									 )
AND PASC_AMC_ExternalType ='AMFI'									 
									 
SET @RejectedCount= @RejectedCount + @RejectedCout1;

 END

Select @RejectedCount

END

ELSE IF(@Flag='N')

  BEGIN
  

---- Getting Records

IF(@AssetGroup ='Equity')

  BEGIN
      IF(@Exchange='NSE')
      BEGIN
	   Select  A.Symbol as RejectedRecordCode ,A.Symbol as RejectedRecordName ,'NSE' as Exchange ,'No NSE Mapping in WERP Mapping Table ' as Reason 
	   from  pcg1server.[MarketData_db].[dbo].[NSEEquities] A
	   where Symbol NOT IN (Select PESM_Identifier from ProductEquityScripMapping)
	   AND [Date]= @startDate
	  END 
	   
	   -- Selecting Records from BSE Table for which there is no ,mapping in Mapping Table 
	   IF(@Exchange='BSE')
	   BEGIN
	   Select Cast(A.SC_CODE AS Varchar(25))  as RejectedRecordCode , A.SC_Name as RejectedRecordName,'BSE' as Exchange, 'No BSE Mapping in WERP Mapping Table' as Reason
	   from pcg1server.[MarketData_db].[dbo].[BSEEquities] A
	   where Cast(A.SC_CODE AS Varchar(25)) NOT IN (Select PESM_Identifier from ProductEquityScripMapping)
	   AND [Date]= @startDate
	   END
       
       -- Selecting Records From Mapping Table for which there is no price either in NSE Or BSE'
       IF(@Exchange='WERP')
       BEGIN
         Select PESM_Identifier as RejectedRecordCode ,PESM_IdentifierName as RejectedRecordName,'WERP'as Exchange ,'No Mapping in NSE OR BSE' as Reason
       from  ProductEquityScripMapping    
       where PESM_IdentifierName in ('NSE', 'BSE') AND (PESM_Identifier NOT IN (
							Select Symbol from pcg1server.[MarketData_db].[dbo].[NSEEquities]  
							where [Date] =@startDate
							 )
		AND PESM_Identifier NOT IN(
							Select CAST(SC_CODE AS Varchar(25)) from pcg1server.[MarketData_db].[dbo].[BSEEquities]
							where [Date]= @startDate
							))
      END							
  END
  
 ELSE IF(@AssetGroup='MF')
 
  BEGIN
   Insert into @RejectedTable(RejectedRecordCode,RejectedRecordName,Exchange ,Reason)
   Select A.Scheme_Code as RejectedRecordCode,Scheme_Name as RejectedRecordName,'AMFI'as Exchange , 'No Mapping in Mapping Table'
   from pcg1server.[MarketData_db].[dbo].[AMFIMarketData] A
   where Scheme_Code NOT IN (Select PASC_AMC_ExternalCode from [wealtherpV1].[dbo].ProductAMCSchemeMapping)
   AND [Date]= @startDate
  
  Insert into @RejectedTable(RejectedRecordCode,RejectedRecordName,Exchange ,Reason)
  Select A.PASP_SchemePlanCode as RejectedRecordCode,B.PASP_SchemePlanName as RejectedRecordName,'WERP' as Exchange ,'No Mapping in MF Table'
  from  [wealtherpV1].[dbo].ProductAMCSchemeMapping A
  inner join ProductAMCSchemePlan B on B.PASP_SchemePlanCode = A.PASP_SchemePlanCode
  where  A.PASC_AMC_ExternalCode NOT IN (
										Select Scheme_Code  from  pcg1server.[MarketData_db].[dbo].[AMFIMarketData]
	
							 ) 
 AND PASC_AMC_ExternalType ='AMFI'								 
  	Select * from @RejectedTable	
  END
  
  END

END
 