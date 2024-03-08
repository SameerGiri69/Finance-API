﻿using Finshark_API.DTOs.Comment;
using Finshark_API.Interfaces;
using Finshark_API.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace Finshark_API.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : Controller
    {
        private readonly ICommentInterface _commentInterface;
        private readonly IStockInterface _stockInterface;

        public CommentController(ICommentInterface commentInterface, IStockInterface stockInterface)
        {
            _commentInterface = commentInterface;
            _stockInterface = stockInterface;
        }
        [HttpGet]
        public async Task<IActionResult> GetComments()
        {
            var comments = await _commentInterface.GetCommentsAsync();

            var commentDto = comments.Select(s => s.ToCommentDtoFromComment());
            return Ok(commentDto);
        }
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetCommentById([FromRoute] int id)
        {
            var comment = await _commentInterface.GetByIdAsync(id);
            return Ok(comment.ToCommentDtoFromComment());

        }
        [HttpPost("{stockId:int}")]
        public async Task<IActionResult> Create([FromRoute] int stockId, WriteCommentDto commentDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if(!await _stockInterface.StockExists(stockId))
            {
                return BadRequest("Stock does not exists");
            }
            var commentModel = commentDto.ToCommentFromWrite(stockId);

            var result = await _commentInterface.WriteCommentAsync(commentModel);

            return Ok(result);
        }
        [HttpDelete("{commentId:int}")]
        public async Task<IActionResult> Delete([FromRoute] int commentId)
        {
            var result = await _commentInterface.DeleteCommentAsync(commentId);
            if (result == false) return BadRequest("Comment Does not exist");
            return Ok("Comment Deleted SuccessFully");
        }
        [HttpPut("{commentId}")]
        public async Task<IActionResult> Update([FromRoute] int commentId, UpdateCommentDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var cExists = await _commentInterface.CommentExistsAsync(commentId);
            if (cExists == null) return NotFound("Could not find specified comment");

            var commentModel = await _commentInterface.UpdateCommentAsync(cExists, dto);

            return View(commentModel.ToCommentDtoFromComment());
        }
    }
}
