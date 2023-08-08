﻿using QuanLyMoiTruong.Application.Interfaces;
using QuanLyMoiTruong.Data.Entities;
using QuanLyMoiTruong.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using QuanLyMoiTruong.Application.ViewModels;
using QuanLyMoiTruong.UnitOfWork.Collections;
using System;
using System.Globalization;
using System.Reflection.Metadata;
using QuanLyMoiTruong.Application.Requests;
using System.Linq.Expressions;
using QuanLyMoiTruong.Common.Expressions;
using QuanLyMoiTruong.Common.Enums;
using QuanLyMoiTruong.Application.Request;

namespace QuanLyMoiTruong.Application.Services
{

    public class BaoCaoBaoVeMoiTruongService : IBaoCaoBaoVeMoiTruongService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileTaiLieuService _fileTaiLieuService;

        public BaoCaoBaoVeMoiTruongService(IUnitOfWork unitOfWork, IFileTaiLieuService fileTaiLieuService)
        {
            _unitOfWork= unitOfWork;
            _fileTaiLieuService= fileTaiLieuService;
        }

        public async Task<ApiResult<bool>> Delete(int id)
        {
            var entity =  await _unitOfWork.GetRepository<BaoCaoBaoVeMoiTruong>().FindAsync(id);
            if (entity != null)
            {
                entity.IsDeleted= true;
                await _unitOfWork.SaveChangesAsync();
                return new ApiSuccessResult<bool>() {};
            }
            else
            {
                return new ApiErrorResult<bool>("Không tồn tại dữ liệu cần xóa") ;
            }
        } 

        public async Task<ApiResult<IList<BaoCaoBaoVeMoiTruongViewModel>>> GetAll()
        {
            var result = new List<BaoCaoBaoVeMoiTruongViewModel>();
            var entities =  await _unitOfWork.GetRepository<BaoCaoBaoVeMoiTruong>().GetAllAsync(predicate: x => !x.IsDeleted);
            result = entities.Select(MapEntityToViewModel).ToList();
            return new ApiSuccessResult<IList<BaoCaoBaoVeMoiTruongViewModel>>() { Data = result };
        }

        public async Task<ApiResult<BaoCaoBaoVeMoiTruongViewModel>> GetById(int id)
        {
            var result = new BaoCaoBaoVeMoiTruongViewModel();
            var data = await _unitOfWork.GetRepository<BaoCaoBaoVeMoiTruong>().GetFirstOrDefaultAsync(predicate: x => x.IdBaoCaoBaoVeMoiTruong == id, include: x => x.Include(y => y.DuAn).Include(y=> y.KhuCongNghiep));
            result = MapEntityToViewModel(data);
            var dsFile = await _fileTaiLieuService.GetByTaiLieu(data.IdBaoCaoBaoVeMoiTruong, NhomTaiLieuEnum.BaoCaoBaoVeMoiTruong.ToString());
            if (dsFile.Success)
            {
                result.FileTaiLieu = dsFile.Data.ToList();
            }
            return new ApiSuccessResult<BaoCaoBaoVeMoiTruongViewModel>() {Data = result };
        }

        public async Task<ApiResult<BaoCaoBaoVeMoiTruong>> Insert(BaoCaoBaoVeMoiTruongViewModel obj)
        {
            var entity = new BaoCaoBaoVeMoiTruong();
            entity = MapViewModelToEntity(obj);
            await _unitOfWork.GetRepository<BaoCaoBaoVeMoiTruong>().InsertAsync(entity);
            await _unitOfWork.SaveChangesAsync();

            var fileTaiLieuRequest = new FileTaiLieuRequest();
            fileTaiLieuRequest.IdTaiLieu = entity.IdBaoCaoBaoVeMoiTruong;
            fileTaiLieuRequest.NhomTaiLieu = NhomTaiLieuEnum.BaoCaoBaoVeMoiTruong.ToString();
            fileTaiLieuRequest.FileTaiLieu = obj.FileTaiLieu;
            await _fileTaiLieuService.UpdateAll(fileTaiLieuRequest);
            return new ApiSuccessResult<BaoCaoBaoVeMoiTruong>() { Data = entity };
        }

        public async Task<ApiResult<bool>> Remove(int id)
        {
            var entity = await _unitOfWork.GetRepository<BaoCaoBaoVeMoiTruong>().FindAsync(id);
            if (entity != null)
            {
                _unitOfWork.GetRepository<BaoCaoBaoVeMoiTruong>().Remove(id);
                return new ApiSuccessResult<bool>() { };
            }
            else
            {
                return new ApiErrorResult<bool>("Không tồn tại dữ liệu cần xóa");
            }
        }

        public async Task<ApiResult<IPagedList<BaoCaoBaoVeMoiTruongViewModel>>> GetAllPaging(BaoCaoBaoVeMoiTruongRequest request)
        {
            Expression<Func<BaoCaoBaoVeMoiTruong, bool>> filter = x => true;
            if (!string.IsNullOrWhiteSpace(request.FullTextSearch))
            {
                var fullTextSearch = request.FullTextSearch.ToLowerInvariant();
                filter = filter.And(p => p.TenBaoCao.ToLower().Contains(fullTextSearch));
            }
            filter = filter.And(p => !p.IsDeleted);
            var data = await _unitOfWork.GetRepository<BaoCaoBaoVeMoiTruong>().GetPagedListAsync(predicate: filter, pageIndex: request.PageIndex, pageSize: request.PageSize);
            data.Items.Select(MapEntityToViewModel);
            var result = new PagedList<BaoCaoBaoVeMoiTruongViewModel>();

            result.PageIndex = data.PageIndex;
            result.PageSize = data.PageSize;
            result.IndexFrom= data.IndexFrom;
            result.TotalCount = data.TotalCount;
            result.TotalPages = data.TotalPages;    
            result.Items = data.Items.Select(MapEntityToViewModel).ToList();
            return new ApiSuccessResult<IPagedList<BaoCaoBaoVeMoiTruongViewModel>>() { Data = result };
        }

        public async Task<ApiResult<BaoCaoBaoVeMoiTruong>> Update(BaoCaoBaoVeMoiTruongViewModel obj)
        {
            var entity = new BaoCaoBaoVeMoiTruong();
            entity = MapViewModelToEntity(obj);
            _unitOfWork.GetRepository<BaoCaoBaoVeMoiTruong>().Update(entity);
            await _unitOfWork.SaveChangesAsync();

            var fileTaiLieuRequest = new FileTaiLieuRequest();
            fileTaiLieuRequest.IdTaiLieu = entity.IdBaoCaoBaoVeMoiTruong;
            fileTaiLieuRequest.NhomTaiLieu = NhomTaiLieuEnum.BaoCaoBaoVeMoiTruong.ToString();
            fileTaiLieuRequest.FileTaiLieu = obj.FileTaiLieu;
            await _fileTaiLieuService.UpdateAll(fileTaiLieuRequest);

            return new ApiSuccessResult<BaoCaoBaoVeMoiTruong>() { Data = entity };
        }
        public async Task<ApiResult<IList<BaoCaoBaoVeMoiTruongViewModel>>> GetListBaoCaoBaoVeMoiTruongByDuAn(int idDuAn)
        {
            var result = new List<BaoCaoBaoVeMoiTruongViewModel>();
            var entities = await _unitOfWork.GetRepository<BaoCaoBaoVeMoiTruong>().GetAllAsync(predicate: x => !x.IsDeleted && x.IdDuAn == idDuAn, include: x => x.Include(x => x.DuAn));
            result = entities.Select(MapEntityToViewModel).ToList();
            foreach (var item in result)
            {
                var dsFile = await _fileTaiLieuService.GetByTaiLieu(item.IdBaoCaoBaoVeMoiTruong, NhomTaiLieuEnum.BaoCaoBaoVeMoiTruong.ToString());
                if (dsFile.Success)
                {
                    item.FileTaiLieu = dsFile.Data.ToList();
                }
            }
            return new ApiSuccessResult<IList<BaoCaoBaoVeMoiTruongViewModel>>() { Data = result };
        }

        public BaoCaoBaoVeMoiTruongViewModel MapEntityToViewModel(BaoCaoBaoVeMoiTruong entity) {
            var result = new BaoCaoBaoVeMoiTruongViewModel();
            result.IdBaoCaoBaoVeMoiTruong = entity.IdBaoCaoBaoVeMoiTruong;
            result.TenBaoCao = entity.TenBaoCao;
            result.NgayBaoCao = entity.NgayBaoCao != null ? entity.NgayBaoCao.Value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "";
            result.IdDuAn = entity.IdDuAn;
            result.TenDuAn = entity.IdDuAn != null? entity.DuAn.TenDuAn: "";
            result.TenDoanhNghiep = entity.IdDuAn != null ? entity.DuAn.TenDoanhNghiep : "";
            result.IdKhuCongNghiep = entity.IdKhuCongNghiep;
            result.TenKhuCongNghiep = entity.IdKhuCongNghiep != null ? entity.KhuCongNghiep.TenKhuCongNghiep : "";
            result.TenChuDauTu = entity.IdKhuCongNghiep != null ? entity.KhuCongNghiep.TenChuDauTu : "";

            return result;
        }
        public BaoCaoBaoVeMoiTruong MapViewModelToEntity(BaoCaoBaoVeMoiTruongViewModel viewModel)
        {
            var entity = new BaoCaoBaoVeMoiTruong();
            entity.IdBaoCaoBaoVeMoiTruong = viewModel.IdBaoCaoBaoVeMoiTruong;
            entity.TenBaoCao = viewModel.TenBaoCao;
            entity.NgayBaoCao = string.IsNullOrEmpty(viewModel.NgayBaoCao) ? null : DateTime.Parse(viewModel.NgayBaoCao, new CultureInfo("vi-VN"));
            entity.IdDuAn = viewModel.IdDuAn;
            entity.IdKhuCongNghiep = viewModel.IdKhuCongNghiep;

            return entity;
        }
    }

}
