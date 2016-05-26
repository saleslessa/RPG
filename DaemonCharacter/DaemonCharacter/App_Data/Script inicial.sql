USE [DaemonCharacter]
GO
begin tran

--delete from tb_attribute_type
--delete from tb_Attribute

--delete from tb_atribute_bonus
--delete from tb_race

--delete from tb_gender
--delete from tb_nonplayer_type

--delete from tb_campaign

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


INSERT INTO tb_race(name) values ('Humano')
INSERT INTO tb_race(name) values ('Doido')
INSERT INTO tb_race(name) values ('Doido de pedra')


INSERT INTO tb_gender(name) values('Male')
INSERT INTO tb_gender(name) values('Female')
INSERT INTO tb_gender(name) values('Other')

insert into tb_nonplayer_type(name) values ('NPC')
insert into tb_nonplayer_type(name) values ('Enemy')
insert into tb_nonplayer_type(name) values ('Side Quest')


INSERT INTO tb_campaign (name, shortDescription, briefing, startYear, maxPlayers, remainingPlayers, idMaster) 
values ('campanha doida', 'bla bla bla', 'ble ble ble', 5, 5, 5,1)


INSERT INTO tb_campaign (name, shortDescription, briefing, startYear, maxPlayers, remainingPlayers, idMaster) 
values ('outra campanha', 'bla bla bla', 'ble ble ble', 3, 3, 3, 1)

INSERT INTO tb_campaign (name, shortDescription, briefing, startYear, maxPlayers, remainingPlayers, idMaster) 
values ('essa nao eh pra ver', 'bla bla bla', 'ble ble ble', 3, 3, 3, 2)


if(@@error <> 0)
begin
	rollback tran
end
else
begin
	commit tran
end

USE [master]

