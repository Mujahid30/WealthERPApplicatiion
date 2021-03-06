/****** Object:  StoredProcedure [dbo].[SP_CreateGovtSavingsAccountAssociation]    Script Date: 06/24/2009 11:13:27 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_CreateGovtSavingsAccountAssociation]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_CreateGovtSavingsAccountAssociation]
GO
/****** Object:  StoredProcedure [dbo].[SP_CreateGovtSavingsAccountAssociation]    Script Date: 06/24/2009 11:13:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_CreateGovtSavingsAccountAssociation]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_CreateGovtSavingsAccountAssociation]
@CGSA_AccountId INT,
@CA_AssociationId INT,
@CGSAA_AssociationType VARCHAR(30),
@CGSAA_CreatedBy INT,
@CGSAA_ModifiedBy INT
AS
INSERT INTO CustomerGovtSavingAccountAssociates 
(

CGSA_AccountId,
CA_AssociationId,
CGSAA_AssociationType,
CGSAA_CreatedBy,
CGSAA_CreatedOn,
CGSAA_ModifiedBy,
CGSAA_ModifiedOn
)
VALUES
(

@CGSA_AccountId,
@CA_AssociationId,
@CGSAA_AssociationType,
@CGSAA_CreatedBy,
CURRENT_TIMESTAMP,
@CGSAA_ModifiedBy,
CURRENT_TIMESTAMP
)
' 
END
GO
