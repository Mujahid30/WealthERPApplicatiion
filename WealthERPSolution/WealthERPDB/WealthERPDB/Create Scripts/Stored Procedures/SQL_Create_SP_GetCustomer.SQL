/****** Object:  StoredProcedure [dbo].[SP_GetCustomer]    Script Date: 06/24/2009 11:59:01 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetCustomer]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_GetCustomer]
GO
/****** Object:  StoredProcedure [dbo].[SP_GetCustomer]    Script Date: 06/24/2009 11:59:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_GetCustomer]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SP_GetCustomer]
@C_CustomerId bigint
as
select * from Customer where C_CustomerId=@C_CustomerId' 
END
GO
