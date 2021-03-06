/****** Object:  StoredProcedure [dbo].[SP_CreateEQTradeDPAssociation]    Script Date: 06/24/2009 11:13:11 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_CreateEQTradeDPAssociation]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_CreateEQTradeDPAssociation]
GO
/****** Object:  StoredProcedure [dbo].[SP_CreateEQTradeDPAssociation]    Script Date: 06/24/2009 11:13:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_CreateEQTradeDPAssociation]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_CreateEQTradeDPAssociation]
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

' 
END
GO
