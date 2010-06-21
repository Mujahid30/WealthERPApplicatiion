-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_CreateEQTradeDPAssociation]
@CEDA_DematAccountId INT,
@CETA_AccountId INT,
@CETDAA_IsDefault TINYINT,
@CETDAA_CreatedBy INT,
@CETDAA_ModifiedBy INT

AS
INSERT INTO CustomerEquityTradeDematAccountAssociation
(
CEDA_DematAccountId,
CETA_AccountId,
CETDAA_IsDefault,
CETDAA_CreatedBy,
CETDAA_CreatedOn,
CETDAA_ModifiedBy,
CETDAA_ModifiedOn
)
VALUES
(
@CEDA_DematAccountId,
@CETA_AccountId,
@CETDAA_IsDefault,
@CETDAA_CreatedBy,
CURRENT_TIMESTAMP,
@CETDAA_ModifiedBy,
CURRENT_TIMESTAMP
)

 