
ALTER PROCEDURE [dbo].[SP_GetPensionAndGratuitiesList]
(
@CP_PortfolioId INT,
@CurrentPage INT =1,
@SortOrder VARCHAR(20) ='CPGNP_OrganizationName ASC'
)
AS
BEGIN
	DECLARE @intStartRow int; 
	DECLARE @intEndRow int;
	SET @intStartRow = (@CurrentPage -1) * 10 + 1;  
	SET @intEndRow = @CurrentPage * 10;

	WITH Entries AS
	(
	SELECT
		CPGN.CPGNP_PensionGratutiesNPId,
		CPGN.CPGA_AccountId,
		CPGN.PAIC_AssetInstrumentCategoryCode,
		CPGN.PAG_AssetGroupCode,
		CPGN.XFY_FiscalYearCode,
		CPGN.CPGNP_EmployeeContri,
		CPGN.CPGNP_EmployerContri,
		CPGN.XF_InterestPayableFrequencyCode,
		CPGN.XF_CompoundInterestFrequencyCode,
		CPGN.CPGNP_InterestRate,
		CASE CPGN.CPGNP_OrganizationName
			WHEN '' THEN 'N/A'
			ELSE CPGN.CPGNP_OrganizationName
		END AS CPGNP_OrganizationName,
		CPGN.CPGNP_DepositAmount,
		CPGN.CPGNP_CurrentValue,
		CPGN.XIB_InterestBasisCode,
		CPGN.CPGNP_IsInterestAccumalated,
		CPGN.CPGNP_InterestAmtAccumalated,
		CPGN.CPGNP_InterestAmtPaidOut,
		CPGN.CPGNP_Remark,
		PC.PAIC_AssetInstrumentCategoryName,
		ROW_NUMBER() over ( ORDER BY
							CASE WHEN @SortOrder = 'OrganizationName DESC'
							THEN CPGNP_OrganizationName END DESC,
							CASE WHEN @SortOrder = 'OrganizationName ASC'
							THEN CPGNP_OrganizationName END ASC,
							CASE WHEN @SortOrder = 'Category DESC'
							THEN PAIC_AssetInstrumentCategoryName END DESC,
							CASE WHEN @SortOrder = 'Category ASC'
							THEN PAIC_AssetInstrumentCategoryName END ASC,
							CASE WHEN @SortOrder = 'DepositAmount DESC'
							THEN CPGN.CPGNP_DepositAmount END DESC,
							CASE WHEN @SortOrder = 'DepositAmount ASC'
							THEN CPGN.CPGNP_DepositAmount END ASC,
							CASE WHEN @SortOrder = 'CurrentValue DESC'
							THEN CPGN.CPGNP_CurrentValue END DESC,
							CASE WHEN @SortOrder = 'CurrentValue ASC'
							THEN CPGN.CPGNP_CurrentValue END ASC
						) as RowNum 
		
		
	FROM 
		CustomerPensionandGratuitiesNetPosition AS CPGN
		INNER JOIN
		dbo.CustomerPensionandGratuitiesAccount AS CPGA
		ON CPGN.CPGA_AccountId = CPGA.CPGA_AccountId
		INNER JOIN
		ProductAssetInstrumentCategory AS PC
		ON CPGA.PAIC_AssetInstrumentCategoryCode = PC.PAIC_AssetInstrumentCategoryCode AND CPGA.PAG_AssetGroupCode = PC.PAG_AssetGroupCode
	WHERE 
		CPGA.CP_PortfolioId = @CP_PortfolioId
   )
   
   SELECT * FROM Entries where RowNum BETWEEN @intStartRow AND @intEndRow
   
	SELECT COUNT (*) FROM 
	CustomerPensionandGratuitiesNetPosition AS CPGN
	INNER JOIN dbo.CustomerPensionandGratuitiesAccount AS CPGA
	ON CPGN.CPGA_AccountId = CPGA.CPGA_AccountId
	INNER JOIN ProductAssetInstrumentCategory AS PC
	ON CPGA.PAIC_AssetInstrumentCategoryCode = PC.PAIC_AssetInstrumentCategoryCode AND CPGA.PAG_AssetGroupCode = PC.PAG_AssetGroupCode
	WHERE CPGA.CP_PortfolioId = @CP_PortfolioId 
	
END
 