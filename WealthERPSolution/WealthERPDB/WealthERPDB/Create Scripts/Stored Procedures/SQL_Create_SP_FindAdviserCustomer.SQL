/****** Object:  StoredProcedure [dbo].[SP_FindAdviserCustomer]    Script Date: 06/24/2009 11:15:30 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_FindAdviserCustomer]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_FindAdviserCustomer]
GO
/****** Object:  StoredProcedure [dbo].[SP_FindAdviserCustomer]    Script Date: 06/24/2009 11:15:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_FindAdviserCustomer]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE SP_FindAdviserCustomer
@C_FirstName VARCHAR(50),
@A_AdviserId int

AS

SELECT 
	C_CustomerId
FROM 
	Customer
WHERE  
	C_FirstName LIKE @C_FirstName + ''%'' 
AND 
	AR_RMId IN 
		(
			SELECT 
				AR_RMId 
			FROM 
				AdviserRM 
			WHERE 
				A_AdviserId=@A_AdviserId)' 
END
GO
