/****** Object:  StoredProcedure [dbo].[SP_DeleteCustomerBankDetails]    Script Date: 06/24/2009 11:14:35 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_DeleteCustomerBankDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_DeleteCustomerBankDetails]
GO
/****** Object:  StoredProcedure [dbo].[SP_DeleteCustomerBankDetails]    Script Date: 06/24/2009 11:14:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_DeleteCustomerBankDetails]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE SP_DeleteCustomerBankDetails
@CB_CustBankAccId INT

AS

BEGIN
	
	DELETE FROM CustomerBank
	WHERE CB_CustBankAccId = @CB_CustBankAccId
	
END' 
END
GO
