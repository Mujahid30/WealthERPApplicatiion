/****** Object:  StoredProcedure [dbo].[SP_CreatePropertyAccountAssociation]    Script Date: 06/24/2009 11:13:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_CreatePropertyAccountAssociation]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_CreatePropertyAccountAssociation]
GO
/****** Object:  StoredProcedure [dbo].[SP_CreatePropertyAccountAssociation]    Script Date: 06/24/2009 11:13:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_CreatePropertyAccountAssociation]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_CreatePropertyAccountAssociation]
@CPA_AccountId INT,
@CA_AssociationId INT,
@CPAA_AssociationType VARCHAR(30),
@CPAA_NomineeShare NUMERIC(3,0),
@CPAA_CreatedBy INT,
@CPAA_ModifiedBy INT
AS
INSERT INTO CustomerPropertyAccountAssociates 
(
CPA_AccountId,
CA_AssociationId,
CPAA_AssociationType,
CPAA_NomineeShare,
CPAA_CreatedBy,
CPAA_CreatedOn,
CPAA_ModifiedBy,
CPAA_ModifiedOn
)
VALUES
(
@CPA_AccountId,
@CA_AssociationId,
@CPAA_AssociationType,
@CPAA_NomineeShare,
@CPAA_CreatedBy,
CURRENT_TIMESTAMP,
@CPAA_ModifiedBy,
CURRENT_TIMESTAMP
)
' 
END
GO
