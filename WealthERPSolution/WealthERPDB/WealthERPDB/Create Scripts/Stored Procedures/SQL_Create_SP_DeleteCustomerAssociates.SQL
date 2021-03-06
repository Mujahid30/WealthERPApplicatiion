/****** Object:  StoredProcedure [dbo].[SP_DeleteCustomerAssociates]    Script Date: 06/24/2009 11:14:29 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_DeleteCustomerAssociates]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_DeleteCustomerAssociates]
GO
/****** Object:  StoredProcedure [dbo].[SP_DeleteCustomerAssociates]    Script Date: 06/24/2009 11:14:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_DeleteCustomerAssociates]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE SP_DeleteCustomerAssociates
@CA_AssociationId INT

AS

BEGIN
	
	DELETE FROM dbo.CustomerAssociates
	WHERE CA_AssociationId = @CA_AssociationId
	
END' 
END
GO
