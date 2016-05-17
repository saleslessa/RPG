USE [DaemonCharacter]
GO
begin tran
INSERT INTO [dbo].tb_attribute_type([name],[useBonus],[useModifier], basemodifier) VALUES ('Característica', 0, 1, 10)
INSERT INTO [dbo].tb_attribute_type([name],[useBonus],[useModifier], basemodifier) VALUES ('Perícia', 1, 0, 0)
INSERT INTO [dbo].tb_attribute_type([name],[useBonus],[useModifier], basemodifier) VALUES ('Característica Secundária', 1, 0, 0)




INSERT INTO [dbo].tb_Attribute ([idAttributeType],[name],[description],[minimum]) VALUES (1, 'For', 'description', 10)
INSERT INTO [dbo].tb_Attribute ([idAttributeType],[name],[description],[minimum]) VALUES (1, 'Dex', 'description', 10)
INSERT INTO [dbo].tb_Attribute ([idAttributeType],[name],[description],[minimum]) VALUES (1, 'Int', 'description', 10)
INSERT INTO [dbo].tb_Attribute ([idAttributeType],[name],[description],[minimum]) VALUES (2, 'Empurrar', 'description', 10)
INSERT INTO [dbo].tb_Attribute ([idAttributeType],[name],[description],[minimum]) VALUES (2, 'Pular', 'description', 10)
INSERT INTO [dbo].tb_Attribute ([idAttributeType],[name],[description],[minimum]) VALUES (2, 'Correr', 'description', 10)
INSERT INTO [dbo].tb_Attribute ([idAttributeType],[name],[description],[minimum]) VALUES (2, 'Prestidigitação', 'description', 10)
INSERT INTO [dbo].tb_Attribute ([idAttributeType],[name],[description],[minimum]) VALUES (2, 'Pensar', 'description', 10)
INSERT INTO [dbo].tb_Attribute ([idAttributeType],[name],[description],[minimum]) VALUES (3, 'IP', 'description', 10)
INSERT INTO [dbo].tb_Attribute ([idAttributeType],[name],[description],[minimum]) VALUES (3, 'PM', 'description', 10)
INSERT INTO [dbo].tb_Attribute ([idAttributeType],[name],[description],[minimum]) VALUES (3, 'PH', 'description', 10)

INSERT INTO [dbo].tb_atribute_bonus ([idAttribute],[idAttributeBonusClass]) VALUES (1, 4)
INSERT INTO [dbo].tb_atribute_bonus ([idAttribute],[idAttributeBonusClass]) VALUES (1, 5)
INSERT INTO [dbo].tb_atribute_bonus ([idAttribute],[idAttributeBonusClass]) VALUES (1, 6)
INSERT INTO [dbo].tb_atribute_bonus ([idAttribute],[idAttributeBonusClass]) VALUES (1, 7)
INSERT INTO [dbo].tb_atribute_bonus ([idAttribute],[idAttributeBonusClass]) VALUES (1, 8)
INSERT INTO [dbo].tb_atribute_bonus ([idAttribute],[idAttributeBonusClass]) VALUES (2, 7)
INSERT INTO [dbo].tb_atribute_bonus ([idAttribute],[idAttributeBonusClass]) VALUES (2, 8)
INSERT INTO [dbo].tb_atribute_bonus ([idAttribute],[idAttributeBonusClass]) VALUES (3, 8)

commit tran

USE [master]

