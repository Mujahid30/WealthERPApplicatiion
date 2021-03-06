/****** Object:  StoredProcedure [dbo].[SP_GetAdviserCustomerListDataSet]    Script Date: 06/24/2009 11:57:37 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetAdviserCustomerListDataSet]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_GetAdviserCustomerListDataSet]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetAdviserCustomerListDataSet]    Script Date: 06/24/2009 11:57:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetAdviserCustomerListDataSet]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetAdviserCustomerListDataSet]
@A_AdviserId INT

AS

BEGIN
	
	SELECT 
		C_CustomerId AS CustomerID,
		C_FirstName+'' ''+C_LastName AS CustomerName,
		U_UMId AS UserID 
	FROM  Customer 
	INNER JOIN dbo.AdviserRM ON dbo.Customer.AR_RMId = dbo.AdviserRM.AR_RMId 
	INNER JOIN dbo.Adviser ON dbo.AdviserRM.A_AdviserId = dbo.Adviser.A_AdviserId
	WHERE dbo.AdviserRM.A_AdviserId=@A_AdviserId
	
	
END


' 
END
GO
