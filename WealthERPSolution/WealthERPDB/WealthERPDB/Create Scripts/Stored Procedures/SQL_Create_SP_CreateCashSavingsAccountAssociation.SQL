/****** Object:  StoredProcedure [dbo].[SP_CreateCashSavingsAccountAssociation]    Script Date: 06/24/2009 11:03:50 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_CreateCashSavingsAccountAssociation]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_CreateCashSavingsAccountAssociation]
GO
/****** Object:  StoredProcedure [dbo].[SP_CreateCashSavingsAccountAssociation]    Script Date: 06/24/2009 11:03:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_CreateCashSavingsAccountAssociation]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_CreateCashSavingsAccountAssociation]
@CCSA_AccountId INT,
@CA_AssociationId INT,
@CCSAA_AssociationType VARCHAR(30),
@CCSAA_CreatedBy INT,
@CCSAA_ModifiedBy INT
AS
INSERT INTO CustomerCashSavingsAccountAssociates 
(
CCSA_AccountId,
CA_AssociationId,
CCSAA_AssociationType,
CCSAA_CreatedBy,
CCSAA_CreatedOn,
CCSAA_ModifiedBy,
CCSAA_ModifiedOn
)
VALUES
(
@CCSA_AccountId,
@CA_AssociationId,
@CCSAA_AssociationType,
@CCSAA_CreatedBy,
CURRENT_TIMESTAMP,
@CCSAA_ModifiedBy,
CURRENT_TIMESTAMP
)
' 
END
GO
