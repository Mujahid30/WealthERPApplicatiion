-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[SP_GetCustomerProof]
@C_CustomerId int
as
select CustomerProof.*, XMLProof.XP_ProofName 
from CustomerProof 
	INNER JOIN 
	XMLProof
ON customerProof.XP_ProofCode=dbo.XMLProof.XP_ProofCode
where C_CustomerId=@C_CustomerId
 