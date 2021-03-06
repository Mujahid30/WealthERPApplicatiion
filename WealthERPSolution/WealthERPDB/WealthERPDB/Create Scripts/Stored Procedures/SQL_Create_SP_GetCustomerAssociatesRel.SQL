/****** Object:  StoredProcedure [dbo].[SP_GetCustomerAssociatesRel]    Script Date: 06/24/2009 11:59:08 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetCustomerAssociatesRel]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_GetCustomerAssociatesRel]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetCustomerAssociatesRel]    Script Date: 06/24/2009 11:59:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetCustomerAssociatesRel]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetCustomerAssociatesRel]
@C_CustomerId INT
AS

BEGIN
	
	SELECT 
		dbo.CustomerAssociates.C_AssociateCustomerId, 
		dbo.CustomerAssociates.CA_AssociationId, 
		dbo.CustomerAssociates.XR_RelationshipCode, 
		dbo.Customer.C_FirstName, 
		dbo.Customer.C_LastName,
		XMLRelationship.XR_Relationship
	FROM 
		dbo.CustomerAssociates
		INNER JOIN
		dbo.Customer
		ON dbo.CustomerAssociates.C_AssociateCustomerId = dbo.Customer.C_CustomerId
		INNER JOIN
		dbo.XMLRelationship
		ON dbo.XMLRelationship.XR_RelationshipCode= dbo.CustomerAssociates.XR_RelationshipCode
	WHERE 
		dbo.CustomerAssociates.C_CustomerId=@C_CustomerId	
	
END
' 
END
GO
