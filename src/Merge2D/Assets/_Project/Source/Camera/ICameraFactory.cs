using System.Threading.Tasks;
using UnityEngine;

namespace Merge2D.Source
{
    public interface ICameraFactory
    {
        Task<Camera> Create(string cameraKey);
    }
}