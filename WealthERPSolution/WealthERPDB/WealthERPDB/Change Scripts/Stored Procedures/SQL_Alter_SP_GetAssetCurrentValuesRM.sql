ALTER PROCEDURE [dbo].[SP_GetAssetCurrentValuesRM]    
@RMID INT    
    
AS    
  
SET NOCOUNT ON

	DECLARE @A_AdviserId INT;
	
	SELECT @A_AdviserId = A_AdviserId
						FROM ViewMutualFundNP
						WHERE AR_RMId = @RMID
     
 /* 1 - Fixed Income Aggregate Current Value*/    
 --SELECT    
 -- 'FI' AS AssetType,     
 -- ISNULL(SUM(CFINP_CurrentValue), 0.0) AS AggrFICurrentValue    
 --FROM    
 -- ViewFixedIncomeNP     
 --WHERE AR_RMId = @RMID    
     
 --UNION    
 /* 2 - Govt Savings Aggregate Current Value*/    
 SELECT    
  'GS' AS AssetType,     
  SUM(CGSNP_CurrentValue) AS AggrGSCurrentValue    
 FROM    
  ViewGovtSavingsNP     
 WHERE AR_RMId = @RMID    
      
     
 UNION    
 /* 3 - Gold Aggregate Current Value*/    
 SELECT    
  'GD' AS AssetType,     
  SUM(CGNP_CurrentValue) AS AggrGDCurrentValue    
 FROM    
  ViewGoldNP     
     
  WHERE AR_RMId = @RMID    
     
 UNION    
 /* 4 - Insurance Aggregate Current Value*/    
 SELECT    
  'IN' AS AssetType,     
  SUM(CINP_SurrenderValue) AS AggrINCurrentValue    
 FROM    
  ViewInsuranceNP    
     
  WHERE AR_RMId = @RMID    
     
 UNION    
 /* 5 - Pension Aggregate Current Value*/    
 SELECT    
  'PG' AS AssetType,     
  SUM(CPGNP_CurrentValue) AS AggrPGCurrentValue    
 FROM    
  ViewPensionGratuities     
     
  WHERE AR_RMId = @RMID    
     
 UNION    
 /* 6 - Property Aggregate Current Value*/    
 SELECT    
  'PR' AS AssetType,     
  SUM(CPNP_CurrentValue) AS AggrPRCurrentValue    
 FROM    
  ViewPropertyNP AS CPP    
     
  WHERE AR_RMId = @RMID    
     
 UNION    
 /* 7 - Cash n Savings Aggregate Current Value*/    
 SELECT    
  'CS' AS AssetType,     
  SUM(CCSNP_CurrentValue) AS AggrCSCurrentValue    
 FROM    
  ViewCashSavingsNP     
      
     
  WHERE AR_RMId = @RMID    
     
 UNION    
 /* 8 - Collectible Aggregate Current Value*/    
 SELECT    
  'CL' AS AssetType,     
  SUM(CCNP_CurrentValue) AS AggrCLCurrentValue    
 FROM    
  ViewCollectiblesNP    
      
 WHERE AR_RMId = @RMID    
     
 UNION    
 /* 9 - Personal Aggregate Current Value*/    
 SELECT    
  'PI' AS AssetType,     
  SUM(CPNP_CurrentValue) AS AggrPICurrentValue    
 FROM    
  ViewPersonalNP    
      
 WHERE AR_RMId = @RMID    
     
 UNION    
 /* 9 - MF Aggregate Current Value*/    
 SELECT    
  'MF' AS AssetType,     
  SUM(CMFNP_CurrentValue) AS AggrMFCurrentValue    
 FROM    
  ViewMutualFundNP     
 WHERE 
	AR_RMId = @RMID 
	AND
	CMFNP_ValuationDate in 
				(
				   SELECT 
						top 1(ADEL_ProcessDate)  
				   FROM    
						dbo.AdviserDailyEODLog    
				   WHERE    
						A_AdviserId = @A_AdviserId 
						AND 
						ADEL_AssetGroup = 'MF'  
					order by ADEL_ProcessDate desc  
				)    
     
 UNION    
 /* 10 - Equity Aggregate Current Value*/    
	SELECT    
		'Equity' AS AssetType,     
		SUM(CENP_CurrentValue) AS AggrEQCurrentValue    
	FROM    
		ViewEquityNP       
	WHERE 
		AR_RMId = @RMID
		and  
		CENP_ValuationDate in 
		(  
			   select top 1(ADEL_ProcessDate)  
			   FROM    
			   dbo.AdviserDailyEODLog    
			   WHERE    
				A_AdviserId = @A_AdviserId  
				AND 
				ADEL_AssetGroup='EQ'  
			   order by ADEL_ProcessDate desc  
		 )        
      
 UNION    
 /* 11 - MF-Hybrid Aggregate Current Value*/    
 SELECT    
	'MF-HY' AS AssetType,     
	SUM(CMFNP_CurrentValue) AS AggrMFHybridCurrentValue    
 FROM    
	ViewMutualFundNP     
 WHERE 
	AR_RMId = @RMID 
	AND 
	CMFNP_ValuationDate IN 
					(
						SELECT 
							top 1(ADEL_ProcessDate)  
					   FROM    
							dbo.AdviserDailyEODLog    
					   WHERE    
							A_AdviserId = @A_AdviserId 
							AND 
							ADEL_AssetGroup = 'MF'  
						order by ADEL_ProcessDate desc
					) 
	AND
	PAIC_AssetInstrumentCategoryCode = 'MFHY'    
     
 UNION    
 /* 11 - MF-Equity Aggregate Current Value*/    
 SELECT    
		'MF-EQ' AS AssetType,     
		SUM(CMFNP_CurrentValue) AS AggrMFEquityCurrentValue    
 FROM    
		ViewMutualFundNP     
 WHERE 
 AR_RMId = @RMID 
 AND
 CMFNP_ValuationDate IN 
					(
						SELECT 
							top 1(ADEL_ProcessDate)  
					   FROM    
							dbo.AdviserDailyEODLog    
					   WHERE    
							A_AdviserId = @A_AdviserId 
							AND 
							ADEL_AssetGroup = 'MF'  
						order by ADEL_ProcessDate desc
					) 
					AND
					PAIC_AssetInstrumentCategoryCode = 'MFEQ'    
     
 UNION    
 /* 11 - MF-Debt Aggregate Current Value*/    
 SELECT    
		'MF-DT' AS AssetType,     
		SUM(CMFNP_CurrentValue) AS AggrMFDebtCurrentValue    
 FROM    
		ViewMutualFundNP     
 WHERE 
		AR_RMId = @RMID 
		AND
		CMFNP_ValuationDate IN (
									SELECT 
										top 1(ADEL_ProcessDate)  
									FROM    
										dbo.AdviserDailyEODLog    
									WHERE    
										A_AdviserId = @A_AdviserId 
										AND 
										ADEL_AssetGroup = 'MF'  
									order by ADEL_ProcessDate desc
								) 
								AND
								PAIC_AssetInstrumentCategoryCode = 'MFDT'    
      
 SET NOCOUNT OFF    