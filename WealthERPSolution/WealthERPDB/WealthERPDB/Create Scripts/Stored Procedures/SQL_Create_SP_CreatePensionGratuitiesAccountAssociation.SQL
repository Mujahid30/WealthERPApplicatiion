/****** Object:  StoredProcedure [dbo].[SP_CreatePensionGratuitiesAccountAssociation]    Script Date: 06/24/2009 11:13:48 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_CreatePensionGratuitiesAccountAssociation]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_CreatePensionGratuitiesAccountAssociation]
GO
/****** Object:  StoredProcedure [dbo].[SP_CreatePensionGratuitiesAccountAssociation]    Script Date: 06/24/2009 11:13:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_CreatePensionGratuitiesAccountAssociation]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_CreatePensionGratuitiesAccountAssociation]
@CPGA_AccountId INT,
@CA_AssociationId INT,
@CPGAA_AssociationType VARCHAR(30),
@CPGAA_NomineeShare NUMERIC(3,0),
@CPGAA_CreatedBy INT,
@CPGAA_ModifiedBy INT
AS
INSERT INTO CustomerPensionandGrauitiesAccountAssociates 
(
CPGA_AccountId,
CA_AssociationId,
CPGAA_AssociationType,
CPGAA_NomineeShare,
CPGAA_CreatedBy,
CPGAA_CreatedOn,
CPGAA_ModifiedBy,
CPGAA_ModifiedOn
)
VALUES
(
@CPGA_AccountId,
@CA_AssociationId,
@CPGAA_AssociationType,
@CPGAA_NomineeShare,
@CPGAA_CreatedBy,
CURRENT_TIMESTAMP,
@CPGAA_ModifiedBy,
CURRENT_TIMESTAMP
)
' 
END
GO
