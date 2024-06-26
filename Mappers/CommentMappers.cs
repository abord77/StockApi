﻿using LearningApi.DTOs.Comments;
using LearningApi.Models;

namespace LearningApi.Mappers {
    public static class CommentMappers {
        public static CommentDTO ToCommentDTO(this Comment commentModel) {
            return new CommentDTO {
                Id = commentModel.Id,
                Title = commentModel.Title,
                Content = commentModel.Content,
                CreatedOn = commentModel.CreatedOn,
                StockId = commentModel.StockId,
            };
        }

        public static Comment ToCommentFromCreate(this CreateCommentDto commentDto, int stockId) {
            return new Comment {
                Title = commentDto.Title,
                Content = commentDto.Content,
                StockId = stockId,
            };
        }
    }
}
