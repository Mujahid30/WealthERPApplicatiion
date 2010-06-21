-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE SP_FindAdviserCustomer
@C_FirstName VARCHAR(50),
@A_AdviserId int

AS

SELECT 
	C_CustomerId
FROM 
	Customer
WHERE  
	C_FirstName LIKE @C_FirstName + '%' 
AND 
	AR_RMId IN 
		(
			SELECT 
				AR_RMId 
			FROM 
				AdviserRM 
			WHERE 
				A_AdviserId=@A_AdviserId) 