﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using User.Domain.Interface.Entities;
using User.Domain.Business.DTO;

namespace User.Domain.Interface.Mapper
{
    public class UserOwnerMapper
    {
        public static UserOwnerDTO MapDTO(UserEntity dto)
        {
            var model = new UserOwnerDTO();
            if (dto != null)
                model = AttributesDTO(dto);
            return null;
        }
        public static List<UserOwnerDTO> MapListDTO(List<UserEntity> entity)
        {
            List<UserOwnerDTO> users = new();
            if (entity.Count > 0)
                foreach (UserEntity item in entity)
                    users.Add(AttributesDTO(item));
            return users;
        }

        public static UserEntity MapEntity(UserOwnerDTO dto)
        {
            var model = new UserEntity();
            if (dto != null)
                model = AttributesEntity(dto);
            return model;
        }
        public static List<UserEntity> MapListEntity(List<UserOwnerDTO> entity)
        {
            List<UserEntity> users = new();
            if (entity.Count > 0)
            {
                foreach (UserOwnerDTO item in entity)
                    users.Add(AttributesEntity(item));
            }
            return users;
        }

        private static UserOwnerDTO AttributesDTO(UserEntity entity)
        {
            var dto = new UserOwnerDTO()
            {
                Id = entity.Id,
                Documento = entity.Documento,
                Nombre = entity.Nombre,
                Apellido = entity.Apellido,
                Celular = entity.Celular,
                Correo = entity.Correo,
                Clave = entity.Clave,
                IdRol = entity.IdRol,
                NombreRol = entity.Rol.Nombre
            };
            return dto;
        }
        private static UserEntity AttributesEntity(UserOwnerDTO dto)
        {
            var user = new UserEntity()
            {
                Id = dto.Id,
                Documento = dto.Documento,
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                Celular = dto.Celular,
                Correo = dto.Correo,
                Clave = dto.Clave,
                IdRol = Convert.ToInt32(dto.IdRol),
            };
            return user;
        }
    }
}
