/****** Object:  StoredProcedure [dbo].[SP_FindRMCustomer]    Script Date: 06/24/2009 11:15:37 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_FindRMCustomer]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_FindRMCustomer]
GO
/****** Object:  StoredProcedure [dbo].[SP_FindRMCustomer]    Script Date: 06/24/2009 11:15:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_FindRMCustomer]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_FindRMCustomer]
@C_FirstName VARCHAR(50),
@AR_RMId int

AS

SELECT 
	C_CustomerId
FROM 
	Customer
WHERE  
	C_FirstName LIKE @C_FirstName + ''%'' 
AND 
	AR_RMId =@AR_RMId
	
	
	-- exec SP_FindRMCustomer ''C'',1098' 
END
GO
