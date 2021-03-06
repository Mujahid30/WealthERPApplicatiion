/****** Object:  StoredProcedure [dbo].[SP_CreateInsuranceAccount]    Script Date: 06/24/2009 11:13:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_CreateInsuranceAccount]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_CreateInsuranceAccount]
GO
/****** Object:  StoredProcedure [dbo].[SP_CreateInsuranceAccount]    Script Date: 06/24/2009 11:13:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_CreateInsuranceAccount]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_CreateInsuranceAccount]

@CP_PortfolioId	INT,
@PAIC_AssetInstrumentCategoryCode	varchar(4),
@PAG_AssetGroupCode	varchar(2),
@CIA_PolicyNum  VARCHAR(30),
@CIA_AccountNum VARCHAR(30),
@CIA_CreatedBy	INT,
@CIA_ModifiedBy	INT,
@AccountId INT OUTPUT

AS
INSERT INTO dbo.CustomerInsuranceAccount (
	CP_PortfolioId,
	PAIC_AssetInstrumentCategoryCode,
	PAG_AssetGroupCode,
	CIA_PolicyNum,
	CIA_AccountNum,
	CIA_CreatedBy,
	CIA_CreatedOn,
	CIA_ModifiedBy,
	CIA_ModifiedOn
) 
VALUES
 (  @CP_PortfolioId,
@PAIC_AssetInstrumentCategoryCode,
@PAG_AssetGroupCode,
@CIA_PolicyNum,
@CIA_AccountNum,
@CIA_CreatedBy,
CURRENT_TIMESTAMP,
@CIA_ModifiedBy,
CURRENT_TIMESTAMP)
SELECT @AccountId=SCOPE_IDENTITY()
' 
END
GO
