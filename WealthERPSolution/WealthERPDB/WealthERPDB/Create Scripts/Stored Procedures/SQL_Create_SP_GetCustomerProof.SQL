/****** Object:  StoredProcedure [dbo].[SP_GetCustomerProof]    Script Date: 06/24/2009 12:01:31 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetCustomerProof]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_GetCustomerProof]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetCustomerProof]    Script Date: 06/24/2009 12:01:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetCustomerProof]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetCustomerProof]
@C_CustomerId int
as
select CustomerProof.*, XMLProof.XP_ProofName 
from CustomerProof 
	INNER JOIN 
	XMLProof
ON customerProof.XP_ProofCode=dbo.XMLProof.XP_ProofCode
where C_CustomerId=@C_CustomerId
' 
END
GO
