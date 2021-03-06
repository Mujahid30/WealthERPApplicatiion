/****** Object:  StoredProcedure [dbo].[SP_CreateInsuranceAccountAssociation]    Script Date: 06/24/2009 11:13:31 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_CreateInsuranceAccountAssociation]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_CreateInsuranceAccountAssociation]
GO
/****** Object:  StoredProcedure [dbo].[SP_CreateInsuranceAccountAssociation]    Script Date: 06/24/2009 11:13:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_CreateInsuranceAccountAssociation]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_CreateInsuranceAccountAssociation]
@CIA_AccountId INT,
@CA_AssociationId INT,
@CIAA_AssociationType VARCHAR(30),
@CIAA_CreatedBy INT,
@CIAA_ModifiedBy INT
AS
INSERT INTO CustomerInsuranceAccountAssociates 
(
CIA_AccountId,
CA_AssociationId,
CIAA_AssociationType,
CIAA_CreatedBy,
CIAA_CreatedOn,
CIAA_ModifiedBy,
CIAA_ModifiedOn
)
VALUES
(
@CIA_AccountId,
@CA_AssociationId,
@CIAA_AssociationType,
@CIAA_CreatedBy,
CURRENT_TIMESTAMP,
@CIAA_ModifiedBy,
CURRENT_TIMESTAMP
)
' 
END
GO
