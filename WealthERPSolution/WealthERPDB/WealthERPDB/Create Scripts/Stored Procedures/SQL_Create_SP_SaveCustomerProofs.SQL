/****** Object:  StoredProcedure [dbo].[SP_SaveCustomerProofs]    Script Date: 06/24/2009 12:05:37 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_SaveCustomerProofs]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_SaveCustomerProofs]
GO
/****** Object:  StoredProcedure [dbo].[SP_SaveCustomerProofs]    Script Date: 06/24/2009 12:05:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_SaveCustomerProofs]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_SaveCustomerProofs]
@C_CustomerId int,
@XP_ProofCode int,
@CP_CreatedBy int,
@CP_ModifiedBy int
as
insert into CustomerProof(
C_CustomerId ,
XP_ProofCode,
CP_CreatedBy,
CP_CreatedOn,
CP_ModifiedBy,
CP_ModifiedOn
)
 values
(@C_CustomerId ,
@XP_ProofCode,
@CP_CreatedBy,
current_timestamp,
@CP_ModifiedBy,
current_timestamp)
' 
END
GO
