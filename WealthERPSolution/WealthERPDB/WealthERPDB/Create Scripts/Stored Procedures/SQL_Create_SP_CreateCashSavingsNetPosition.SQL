/****** Object:  StoredProcedure [dbo].[SP_CreateCashSavingsNetPosition]    Script Date: 06/24/2009 11:03:52 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_CreateCashSavingsNetPosition]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_CreateCashSavingsNetPosition]
GO
/****** Object:  StoredProcedure [dbo].[SP_CreateCashSavingsNetPosition]    Script Date: 06/24/2009 11:03:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_CreateCashSavingsNetPosition]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_CreateCashSavingsNetPosition]

@PAIC_AssetInstrumentCategoryCode VARCHAR(5),
@PAG_AssetGroupCode VARCHAR(5),
@CCSA_AccountId INT,
@XDI_DebtIssuerCode VARCHAR(5),
@XIB_InterestBasisCode VARCHAR(5),
@XF_CompoundInterestFrequencyCode VARCHAR(5) ,
@XF_InterestPayoutFrequencyCode VARCHAR(5) ,
@CCSNP_Name VARCHAR(50),
@CCSNP_DepositAmount NUMERIC(18,2),
@CCSNP_DepositDate DATETIME,
@CCSNP_CurrentValue NUMERIC(18,3),
@CCSNP_InterestRate NUMERIC(6,3),
@CCSNP_InterestAmntPaidOut NUMERIC(18,3) ,
@CCSNP_IsInterestAccumulated TINYINT,
@CCSNP_InterestAmntAccumulated NUMERIC(18,3),
@CCSNP_CreatedBy INT,
@CCSNP_ModifiedBy INT,
@CCSNP_Remark VARCHAR(100)
AS

BEGIN
	
	INSERT INTO dbo.CustomerCashSavingsNetPosition (
		PAIC_AssetInstrumentCategoryCode,
		PAG_AssetGroupCode,
		CCSA_AccountId,
		XDI_DebtIssuerCode,
		XIB_InterestBasisCode,
		XF_CompoundInterestFrequencyCode,
		XF_InterestPayoutFrequencyCode,
		CCSNP_Name,
		CCSNP_DepositAmount,
		CCSNP_DepositDate,
		CCSNP_CurrentValue,
		CCSNP_InterestRate,
		CCSNP_InterestAmntPaidOut,
		CCSNP_IsInterestAccumulated,
		CCSNP_InterestAmntAccumulated,
		CCSNP_CreatedBy,
		CCSNP_CreatedOn,
		CCSNP_ModifiedBy,
		CCSNP_ModifiedOn,
		CCSNP_Remark
	) VALUES ( 
		 
		 @PAIC_AssetInstrumentCategoryCode,
		 @PAG_AssetGroupCode,
		 @CCSA_AccountId,
		 @XDI_DebtIssuerCode,
		 @XIB_InterestBasisCode,
		 @XF_CompoundInterestFrequencyCode,
		 @XF_InterestPayoutFrequencyCode,
		 @CCSNP_Name,
		 @CCSNP_DepositAmount,
		 @CCSNP_DepositDate,
		 @CCSNP_CurrentValue,
		 @CCSNP_InterestRate,
		 @CCSNP_InterestAmntPaidOut,
		 @CCSNP_IsInterestAccumulated,
		 @CCSNP_InterestAmntAccumulated,
		 @CCSNP_CreatedBy,
		 CURRENT_TIMESTAMP,
		 @CCSNP_ModifiedBy,
		 CURRENT_TIMESTAMP ,
		 @CCSNP_Remark) 
	
END' 
END
GO
