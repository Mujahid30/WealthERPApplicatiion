/****** Object:  StoredProcedure [dbo].[SP_GetRMCustomerListDataSet]    Script Date: 06/24/2009 12:04:43 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetRMCustomerListDataSet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_GetRMCustomerListDataSet]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetRMCustomerListDataSet]    Script Date: 06/24/2009 12:04:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetRMCustomerListDataSet]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetRMCustomerListDataSet]
@AR_RMId INT

AS

BEGIN
	
	SELECT 
		C_CustomerId AS CustomerID,
		C_FirstName+'' ''+C_LastName AS CustomerName,
		U_UMId AS UserID
	FROM
		dbo.Customer
	WHERE
		AR_RMId = @AR_RMId
	
END


' 
END
GO
