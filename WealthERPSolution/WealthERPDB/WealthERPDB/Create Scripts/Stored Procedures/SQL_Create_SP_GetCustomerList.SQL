/****** Object:  StoredProcedure [dbo].[SP_GetCustomerList]    Script Date: 06/24/2009 12:00:21 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetCustomerList]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_GetCustomerList]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetCustomerList]    Script Date: 06/24/2009 12:00:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetCustomerList]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE SP_GetCustomerList

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
						C2.C_FirstName +'' ''+ C2.C_MiddleName +'' ''+C2.C_LastName AS Parent

  
  
  

 FROM Customer C1 LEFT OUTER JOIN CustomerAssociates CA ON C1.C_CustomerId=CA.C_AssociateCustomerId
								  LEFT OUTER JOIN Customer C2 on CA.C_CustomerId=C2.C_CustomerId
				 WHERE C1.AR_RMId=@AR_RMId and C1.C_FirstName like  @C_FirstName +''%''

--Exec SP_GetCustomerList ''R'',1037' 
END
GO
