/****** Object:  StoredProcedure [dbo].[SP_CreateCustomerAssociates]    Script Date: 06/24/2009 11:12:42 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_CreateCustomerAssociates]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_CreateCustomerAssociates]
GO
/****** Object:  StoredProcedure [dbo].[SP_CreateCustomerAssociates]    Script Date: 06/24/2009 11:12:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_CreateCustomerAssociates]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'
CREATE PROCEDURE [dbo].[SP_CreateCustomerAssociates]

@C_AssociateCustomerId int,
@XR_RelationshipCode varchar(5),
@C_CustomerId int,
@CA_CreatedBy	int	,
@CA_ModifiedBy	int	

as
insert into CustomerAssociates
(
C_AssociateCustomerId,
XR_RelationshipCode,
C_CustomerId,
CA_CreatedBy,
CA_CreatedOn,
CA_ModifiedBy,
CA_ModifiedOn) 

values(

@C_AssociateCustomerId,
@XR_RelationshipCode,
@C_CustomerId,
@CA_CreatedBy,
current_timestamp,
@CA_ModifiedBy,
current_timestamp
)
' 
END
GO
