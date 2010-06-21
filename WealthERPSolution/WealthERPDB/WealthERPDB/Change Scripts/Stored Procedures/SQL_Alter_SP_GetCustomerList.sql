-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE SP_GetCustomerList

@C_FirstName varchar(50),
@AR_RMId int

as

select 
						C1.C_CustomerId,
						C1.C_FirstName,
						C1.C_MiddleName,
						C1.C_LastName,
						C1.C_ResISDCode,
						C1.C_ResSTDCode,
						C1.C_ResPhoneNum,
						C1.U_UMId,
						C1.AR_RMId,
						C1.C_CustCode,
						C1.C_CompanyName,
						C1.C_Email,
						CA.C_CustomerId as ParentId,
						C2.C_FirstName +' '+ C2.C_MiddleName +' '+C2.C_LastName AS Parent

  
  
  

 FROM Customer C1 LEFT OUTER JOIN CustomerAssociates CA ON C1.C_CustomerId=CA.C_AssociateCustomerId
								  LEFT OUTER JOIN Customer C2 on CA.C_CustomerId=C2.C_CustomerId
				 WHERE C1.AR_RMId=@AR_RMId and C1.C_FirstName like  @C_FirstName +'%'

--Exec SP_GetCustomerList 'R',1037 