using QuanLyMoiTruong.Application.Interfaces;
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

    public class KetQuaThongKeNguonThaiService : IKetQuaThongKeNguonThaiService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileTaiLieuService _fileTaiLieuService;
        private readonly IDiemQuanTracService _diemQuanTracService;

        public KetQuaThongKeNguonThaiService(IUnitOfWork unitOfWork, IFileTaiLieuService fileTaiLieuService, IDiemQuanTracService diemQuanTracService)
        {
            _unitOfWork = unitOfWork;
            _fileTaiLieuService = fileTaiLieuService;
            _diemQuanTracService = diemQuanTracService;
        }

        public async Task<ApiResult<bool>> Delete(int id)
        {
            var entity = await _unitOfWork.GetRepository<KetQuaThongKeNguonThai>().FindAsync(id);
            if (entity != null)
            {
                entity.IsDeleted = true;
                await _unitOfWork.SaveChangesAsync();
                return new ApiSuccessResult<bool>() { };
            }
            else
            {
                return new ApiErrorResult<bool>("Không tồn tại dữ liệu cần xóa");
            }
        }

        public async Task<ApiResult<IList<KetQuaThongKeNguonThaiViewModel>>> GetAll()
        {
            var result = new List<KetQuaThongKeNguonThaiViewModel>();
            var entities = await _unitOfWork.GetRepository<KetQuaThongKeNguonThai>().GetAllAsync(predicate: x => !x.IsDeleted);
            result = entities.Select(MapEntityToViewModel).ToList();
            return new ApiSuccessResult<IList<KetQuaThongKeNguonThaiViewModel>>() { Data = result };
        }

        public async Task<ApiResult<KetQuaThongKeNguonThaiViewModel>> GetById(int id)
        {
            var result = new KetQuaThongKeNguonThaiViewModel();
            var data = await _unitOfWork.GetRepository<KetQuaThongKeNguonThai>().GetFirstOrDefaultAsync(predicate: x => x.IdKetQuaThongKeNguonThai == id);
            result = MapEntityToViewModel(data);
            return new ApiSuccessResult<KetQuaThongKeNguonThaiViewModel>() { Data = result };
        }

        public async Task<ApiResult<KetQuaThongKeNguonThai>> Insert(KetQuaThongKeNguonThaiViewModel obj)
        {
            var entity = new KetQuaThongKeNguonThai();
            entity = MapViewModelToEntity(obj, entity);
            await _unitOfWork.GetRepository<KetQuaThongKeNguonThai>().InsertAsync(entity);
            await _unitOfWork.SaveChangesAsync();
            return new ApiSuccessResult<KetQuaThongKeNguonThai>() { Data = entity };
        }
        public async Task<ApiResult<bool>> Remove(int id)
        {
            var entity = await _unitOfWork.GetRepository<KetQuaThongKeNguonThai>().FindAsync(id);
            if (entity != null)
            {
                _unitOfWork.GetRepository<KetQuaThongKeNguonThai>().Remove(id);
                return new ApiSuccessResult<bool>() { };
            }
            else
            {
                return new ApiErrorResult<bool>("Không tồn tại dữ liệu cần xóa");
            }
        }

        public async Task<ApiResult<IPagedList<KetQuaThongKeNguonThaiViewModel>>> GetAllPaging(PagingRequest request)
        {
            Expression<Func<KetQuaThongKeNguonThai, bool>> filter = x => true;
            if (!string.IsNullOrWhiteSpace(request.FullTextSearch))
            {
                var fullTextSearch = request.FullTextSearch.ToLowerInvariant();
                filter = filter.And(p => p.TenKhuCongNghiep.ToLower().Contains(fullTextSearch));
            }
            filter = filter.And(p => !p.IsDeleted);
            var data = await _unitOfWork.GetRepository<KetQuaThongKeNguonThai>().GetPagedListAsync(predicate: filter, pageIndex: request.PageIndex, pageSize: request.PageSize);
            data.Items.Select(MapEntityToViewModel);
            var result = new PagedList<KetQuaThongKeNguonThaiViewModel>();

            result.PageIndex = data.PageIndex;
            result.PageSize = data.PageSize;
            result.IndexFrom = data.IndexFrom;
            result.TotalCount = data.TotalCount;
            result.TotalPages = data.TotalPages;
            result.Items = data.Items.Select(MapEntityToViewModel).ToList();
            return new ApiSuccessResult<IPagedList<KetQuaThongKeNguonThaiViewModel>>() { Data = result };
        }

        public async Task<ApiResult<KetQuaThongKeNguonThai>> Update(KetQuaThongKeNguonThaiViewModel obj)
        {
            var entity = await _unitOfWork.GetRepository<KetQuaThongKeNguonThai>().GetFirstOrDefaultAsync(predicate: x => x.IdKetQuaThongKeNguonThai == obj.IdKetQuaThongKeNguonThai);
            entity = MapViewModelToEntity(obj, entity);
            _unitOfWork.GetRepository<KetQuaThongKeNguonThai>().Update(entity);
            await _unitOfWork.SaveChangesAsync();
            return new ApiSuccessResult<KetQuaThongKeNguonThai>() { Data = entity };
        }
        public async Task<ApiResult<bool>> InsertFromExcel(List<KetQuaThongKeNguonThaiViewModel> list)
        {
            try
            {
                foreach (var obj in list)
                {
                    var entity = new KetQuaThongKeNguonThai();
                    entity = MapViewModelToEntity(obj, entity);
                    await _unitOfWork.GetRepository<KetQuaThongKeNguonThai>().InsertAsync(entity);
                }
                await _unitOfWork.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                return new ApiSuccessResult<bool>() { Data = false };
            }
            return new ApiSuccessResult<bool>() { Data = true };
        }
        public async Task<ApiResult<List<KetQuaThongKeNguonThaiViewModel>>> GetAllByIdBaoCaoThongKeNguonThai(int idBaoCaoThongKeNguonThai)
        {
            var result = new List<KetQuaThongKeNguonThaiViewModel>();
            var entities = await _unitOfWork.GetRepository<KetQuaThongKeNguonThai>().GetAllAsync(predicate: x => x.IdBaoCaoThongKeNguonThai == idBaoCaoThongKeNguonThai && !x.IsDeleted);
            result = entities.Select(MapEntityToViewModel).ToList();
            return new ApiSuccessResult<List<KetQuaThongKeNguonThaiViewModel>>() { Data = result };
        }
        public KetQuaThongKeNguonThaiViewModel MapEntityToViewModel(KetQuaThongKeNguonThai entity)
        {
            var result = new KetQuaThongKeNguonThaiViewModel();
            result.IdKetQuaThongKeNguonThai = entity.IdKetQuaThongKeNguonThai;
            result.IdKhuCongNghiep = entity.IdKhuCongNghiep;
            result.TenKhuCongNghiep = entity.TenKhuCongNghiep;
            result.NuocThaiSinhHoat = entity.NuocThaiSinhHoat;
            result.NuocThaiSanXuat = entity.NuocThaiSanXuat;
            result.NuocThaiTaiSuDung = entity.NuocThaiTaiSuDung;
            result.LuuLuongDauNoi = entity.LuuLuongDauNoi;
            result.KhiThai = entity.KhiThai;
            result.ChatThaiRanSinhHoat = entity.ChatThaiRanSinhHoat;
            result.ChatThaiRanSanXuat = entity.ChatThaiRanSanXuat;
            result.ChatThaiRanTaiSuDung = entity.ChatThaiRanTaiSuDung;
            result.TongChatThaiRan = entity.TongChatThaiRan;
            result.ChatThaiNguyHai = entity.ChatThaiNguyHai;
            return result;
        }
        public KetQuaThongKeNguonThai MapViewModelToEntity(KetQuaThongKeNguonThaiViewModel viewModel, KetQuaThongKeNguonThai entity)
        {
            entity.IdKetQuaThongKeNguonThai = viewModel.IdKetQuaThongKeNguonThai;
            entity.IdBaoCaoThongKeNguonThai = viewModel.IdBaoCaoThongKeNguonThai;
            entity.IdKhuCongNghiep = viewModel.IdKhuCongNghiep;
            entity.TenKhuCongNghiep = viewModel.TenKhuCongNghiep;
            entity.NuocThaiSinhHoat = viewModel.NuocThaiSinhHoat;
            entity.NuocThaiSanXuat = viewModel.NuocThaiSanXuat;
            entity.NuocThaiTaiSuDung = viewModel.NuocThaiTaiSuDung;
            entity.KhiThai = viewModel.KhiThai;
            entity.LuuLuongDauNoi = viewModel.LuuLuongDauNoi;
            entity.ChatThaiRanSinhHoat = viewModel.ChatThaiRanSinhHoat;
            entity.ChatThaiRanSanXuat = viewModel.ChatThaiRanSanXuat;
            entity.ChatThaiRanTaiSuDung = viewModel.ChatThaiRanTaiSuDung;
            entity.TongChatThaiRan = viewModel.TongChatThaiRan;
            entity.ChatThaiNguyHai = viewModel.ChatThaiNguyHai;
            return entity;
        }
    }

}
