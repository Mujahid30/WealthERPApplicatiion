/****** Object:  StoredProcedure [dbo].[SP_GetCustomerAssociates]    Script Date: 06/24/2009 11:59:06 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetCustomerAssociates]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_GetCustomerAssociates]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetCustomerAssociates]    Script Date: 06/24/2009 11:59:06 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetCustomerAssociates]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetCustomerAssociates]
@C_CustomerId int
as
select * from CustomerAssociates where C_CustomerId=@C_CustomerId' 
END
GO
